using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Planetside2LCDStats
{
    internal class PlanetsideStatsRetriever
    {
        public enum PanelPage
        {
            KillDeathInfos,
            PlayerInfos
        }

        private readonly string charId;
        private readonly Font littleFont = new Font("Lucida Console", 6, FontStyle.Regular, GraphicsUnit.Point);

        private readonly WebClient wc;
        public BitmapData BmpData;
        private JObject charJObject;
        private string charJson;
        private PanelPage currentPage;
        private int deaths;
        private bool forceRefresh;
        private Graphics fullBitmapGraphics;
        private int kills;
        private int lastEventTimestamp = int.MaxValue;
        private JObject lastEventsAttackerJObject;
        private string lastEventsAttackerJson;
        private JObject lastEventsJObject;
        private string lastEventsJson;
        private string ownFactionId;
        private JObject totalsJObject;
        private string totalsJson;


        public PlanetsideStatsRetriever(string charId)
        {
            this.charId = charId;
            currentPage = PanelPage.KillDeathInfos;
            wc = new WebClient();

            FullBitmap = new Bitmap(160, 43);
            fullBitmapGraphics = Graphics.FromImage(FullBitmap);
        }

        public bool Querying { get; set; }

        public Bitmap FullBitmap { get; set; }

        public PanelPage CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage != value)
                {
                    forceRefresh = true;
                    currentPage = value;
                }
            }
        }

        public bool InitializeStats()
        {
            //// Total kills and deaths
            totalsJson =
                wc.DownloadString("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + charId +
                                  "&stat_name=deaths,kills&c:show=stat_name,all_time&c:limit=100");
            totalsJObject = JObject.Parse(totalsJson);
            if (totalsJObject["returned"].ToString() == "0")
                return false;
            if (totalsJObject["characters_stat_history_list"][0]["stat_name"].ToString() == "kills")
            {
                kills = Int32.Parse(totalsJObject["characters_stat_history_list"][0]["all_time"].ToString());
                deaths = Int32.Parse(totalsJObject["characters_stat_history_list"][1]["all_time"].ToString());
            }
            else
            {
                kills = Int32.Parse(totalsJObject["characters_stat_history_list"][1]["all_time"].ToString());
                deaths = Int32.Parse(totalsJObject["characters_stat_history_list"][0]["all_time"].ToString());
            }
            charJson =
                wc.DownloadString("http://census.soe.com/get/ps2:v2/character/" + charId +
                                  "?c:resolve=currency&c:hide=daily_ribbon,times,head_id,title_id,certs.earned_points,certs.gifted_points,certs.spent_points");
            charJObject = JObject.Parse(charJson);
            ownFactionId = charJObject["character_list"][0]["faction_id"].ToString();
            return true;
        }

        public bool UpdateStats()
        {
            if (Querying) return false;

            Querying = true;

            if (currentPage == PanelPage.KillDeathInfos)
            {
                Debug.WriteLine("Querying for new events...");
                string lastEventDateJson =
                    wc.DownloadString("http://census.soe.com/get/ps2:v2/characters_event/?character_id=" + charId +
                                      "&type=KILL,DEATH&c:limit=1");
                JObject lastEventDateJObject = JObject.Parse(lastEventDateJson);
                int timestamp = Convert.ToInt32(lastEventDateJObject["characters_event_list"][0]["timestamp"].ToString());
                if (timestamp == lastEventTimestamp && !forceRefresh)
                {
                    Querying = false;
                    return false;
                }
                lastEventTimestamp = timestamp;

                // Character info
                charJson =
                    wc.DownloadString("http://census.soe.com/get/ps2:v2/character/" + charId +
                                      "?&c:hide=daily_ribbon,times,head_id,title_id,certs.earned_points,certs.gifted_points,certs.spent_points");
                charJObject = JObject.Parse(charJson);

                // Last Kills and deaths
                lastEventsJson =
                    wc.DownloadString("http://census.soe.com/get/ps2:v2/characters_event/?character_id=" + charId +
                                      "&type=KILL,DEATH&c:limit=20&c:join=character^show:name.first%27faction_id%27profile_id%27battle_rank.value");
                lastEventsJObject = JObject.Parse(lastEventsJson);

                lastEventsAttackerJson =
                    wc.DownloadString("http://census.soe.com/get/ps2:v2/characters_event/?character_id=" + charId +
                                      "&type=KILL,DEATH&c:limit=10&c:join=type:character^on:attacker_character_id^to:character_id^show:name.first%27faction_id%27profile_id%27battle_rank.value");
                lastEventsAttackerJObject = JObject.Parse(lastEventsAttackerJson);

                DrawKillDeathsLCDImage();
                Querying = false;
                forceRefresh = false;
                return true;
            }
            if (currentPage == PanelPage.PlayerInfos)
            {
                Debug.WriteLine("Querying character infos...");
                // Character info
                charJson =
                    wc.DownloadString("http://census.soe.com/get/ps2:v2/character/" + charId +
                                      "?c:resolve=stat&c:hide=daily_ribbon,head_id,title_id,certs.gifted_points,certs.spent_points");
                charJObject = JObject.Parse(charJson);
                // kills and deaths total
                totalsJson =
                    wc.DownloadString("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + charId +
                                      "&stat_name=deaths,kills&c:show=stat_name,all_time&c:limit=100");
                totalsJObject = JObject.Parse(totalsJson);
                if (totalsJObject["characters_stat_history_list"][0]["stat_name"].ToString() == "kills")
                {
                    kills = Int32.Parse(totalsJObject["characters_stat_history_list"][0]["all_time"].ToString());
                    deaths = Int32.Parse(totalsJObject["characters_stat_history_list"][1]["all_time"].ToString());
                }
                else
                {
                    kills = Int32.Parse(totalsJObject["characters_stat_history_list"][1]["all_time"].ToString());
                    deaths = Int32.Parse(totalsJObject["characters_stat_history_list"][0]["all_time"].ToString());
                }
                DrawCharInfosLCDImage();
                Querying = false;
                forceRefresh = false;
                return true;
            }
            Querying = false;
            forceRefresh = false;
            return false;
        }

        private void DrawCharInfosLCDImage()
        {
            string ownDesription = "(" + charJObject["character_list"][0]["battle_rank"]["value"] + ") " +
                                   charJObject["character_list"][0]["name"]["first"];
            string certInfo = "CERTs: " + charJObject["character_list"][0]["certs"]["available_points"];
            string brInfo = "BR: " + charJObject["character_list"][0]["battle_rank"]["value"] + " | Next: " +
                            charJObject["character_list"][0]["battle_rank"]["percent_to_next"] + "%";
            string xpInfo = "XP/hour: ";
            string kdInfo = "K:" + kills + " D:" + deaths + " K/D:" + Math.Round((float) kills/deaths, 2);

            // Initialize image and graphics rendering methods
            FullBitmap = new Bitmap(160, 43);
            fullBitmapGraphics = Graphics.FromImage(FullBitmap);
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 0, 160, 8);
            fullBitmapGraphics.FillRectangle(Brushes.Black, 0, 0, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.Black, 159, 0, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.Black, new Rectangle(0, 9, 160, 43));
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 8, 2, 1);
            fullBitmapGraphics.FillRectangle(Brushes.White, 158, 8, 2, 1);
            // Just some decorations
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 9, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.White, 159, 9, 1, 1);
            fullBitmapGraphics.SmoothingMode = SmoothingMode.None;
            fullBitmapGraphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            // draw header infos
            fullBitmapGraphics.DrawString(ownDesription, littleFont, Brushes.Black, 0, 0);

            fullBitmapGraphics.DrawString(certInfo, littleFont, Brushes.White, 0, 10);
            fullBitmapGraphics.DrawString(brInfo, littleFont, Brushes.White, 0, 18);
            fullBitmapGraphics.DrawString(xpInfo, littleFont, Brushes.White, 0, 26);
            fullBitmapGraphics.DrawString(kdInfo, littleFont, Brushes.White, 0, 34);

            // Copy rendered data into a BitmapData variable with the 8bpp Indexed format (needed to display on LCD screen)
            BmpData = FullBitmap.LockBits(new Rectangle(0, 0, FullBitmap.Width, FullBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
        }

        private void DrawKillDeathsLCDImage()
        {
            string ownDesription = "(" + charJObject["character_list"][0]["battle_rank"]["value"] + ") " +
                                   charJObject["character_list"][0]["name"]["first"];
            ownDesription += " - K/D:" + Math.Round((float) kills/deaths, 2);

            // Initialize image and graphics rendering methods
            FullBitmap = new Bitmap(160, 43);
            fullBitmapGraphics = Graphics.FromImage(FullBitmap);
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 0, 160, 8);
            fullBitmapGraphics.FillRectangle(Brushes.Black, 0, 0, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.Black, 159, 0, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.Black, new Rectangle(0, 9, 160, 43));
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 8, 2, 1);
            fullBitmapGraphics.FillRectangle(Brushes.White, 158, 8, 2, 1);
            // Just some decorations
            fullBitmapGraphics.FillRectangle(Brushes.White, 0, 9, 1, 1);
            fullBitmapGraphics.FillRectangle(Brushes.White, 159, 9, 1, 1);
            // Or a full box
            //fullBitmapGraphics.DrawLine(Pens.White, 0, 9, 0, 43);
            //fullBitmapGraphics.DrawLine(Pens.White, 159, 9, 159, 43);
            //fullBitmapGraphics.DrawLine(Pens.White, 0, 42, 160, 42);

            //fullBitmapGraphics.CompositingQuality = CompositingQuality.HighQuality;
            //fullBitmapGraphics.InterpolationMode= InterpolationMode.HighQualityBicubic;
            //fullBitmapGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            fullBitmapGraphics.SmoothingMode = SmoothingMode.None;
            fullBitmapGraphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            // draw header infos
            fullBitmapGraphics.DrawString(ownDesription, littleFont, Brushes.Black, 0, 0);
            // fullBitmapGraphics.DrawLine(Pens.White, 0, 8, 160, 8);

            for (int i = 0; i < 4; i++)
            {
                JToken eventEntry =
                    lastEventsJObject["characters_event_list"][i];
                JToken eventAttackerEntry = lastEventsAttackerJObject["characters_event_list"][i];
                if (eventEntry["table_type"].ToString() == "deaths")
                {
                    string line = "<-[" +
                                  FactionNameFromID(
                                      eventAttackerEntry["attacker_character_id_join_character"]["faction_id"].ToString()) +
                                  "]" +
                                  eventAttackerEntry["attacker_character_id_join_character"]["name"]["first"] + " (" +
                                  eventAttackerEntry["attacker_character_id_join_character"]["battle_rank"]["value"] +
                                  ")";
                    if (eventAttackerEntry["is_headshot"].ToString() == "1")
                    {
                        line += "-HS ";
                    }
                    fullBitmapGraphics.DrawString(line, littleFont, Brushes.White, 0, 10 + (i*8));
                    if (eventAttackerEntry["attacker_character_id_join_character"]["faction_id"].ToString() ==
                        ownFactionId)
                    {
                        fullBitmapGraphics.FillRectangle(Brushes.White, 1, 10 + (i*8), 10, 6);
                        fullBitmapGraphics.DrawString("<-", littleFont, Brushes.Black, 0, 10 + (i*8));
                    }
                }
                else if (eventEntry["table_type"].ToString() == "kills")
                {
                    string line = "=>[" +
                                  FactionNameFromID(eventEntry["character_id_join_character"]["faction_id"].ToString()) +
                                  "]" +
                                  eventEntry["character_id_join_character"]["name"]["first"] + " (" +
                                  eventEntry["character_id_join_character"]["battle_rank"]["value"] + ")";
                    if (eventEntry["is_headshot"].ToString() == "1")
                    {
                        line += "-HS";
                    }
                    fullBitmapGraphics.DrawString(line, littleFont, Brushes.White, 0, 10 + (i*8));
                    if (eventEntry["character_id_join_character"]["faction_id"].ToString() ==
                        ownFactionId)
                    {
                        fullBitmapGraphics.FillRectangle(Brushes.White, 1, 10 + (i*8), 10, 6);
                        fullBitmapGraphics.DrawString("=>", littleFont, Brushes.Black, 0, 10 + (i*8));
                    }
                }
            }

            // Copy rendered data into a BitmapData variable with the 8bpp Indexed format (needed to display on LCD screen)
            BmpData = FullBitmap.LockBits(new Rectangle(0, 0, FullBitmap.Width, FullBitmap.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
            //lcdBitmap = new Bitmap(160, 43, bmpData.Stride, PixelFormat.Format8bppIndexed, bmpData.Scan0);
            //lcdBitmap.Save(@"K:\test.bmp");
        }

        private string FactionNameFromID(string factionId)
        {
            switch (factionId)
            {
                case "1":
                    return "VS";
                case "2":
                    return "NC";
                case "3":
                    return "TR";
                default:
                    return "";
            }
        }
    }
}