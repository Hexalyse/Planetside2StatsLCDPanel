using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using dd.logilcd;

namespace Planetside2LCDStats
{
    public partial class Main : Form
    {
        private readonly Byte[] bmpBytes = new byte[6880];
        private readonly LogiLcd logilcd = new LogiLcd();
        private int refreshCount;
        private PlanetsideStatsRetriever statsRetriever;
        private Color txtColor = Color.White;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lblStatus.Text = @"[ Not connected ]";
            btnShutdown.Enabled = false;
            btnInit.Enabled = true;
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (textBoxCharId.Text.Length != 19 || !IsDigitsOnly(textBoxCharId.Text))
            {
                MessageBox.Show(@"Please enter a valid character ID (19 digits).");
                return;
            }
            if (!logilcd.Initialize("Planetside 2 Stats by Hexalyse", LCD_TYPE.MONO | LCD_TYPE.COLOR))
            {
                MessageBox.Show(@"Couldn't init Logitecgh LCD :(", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logilcd.Shutdown();
            }
            else
            {
                statsRetriever = new PlanetsideStatsRetriever(textBoxCharId.Text);
                if (statsRetriever.InitializeStats() == false)
                {
                    MessageBox.Show(@"Couldn't get character infos (invalid ID or service unavailable).");
                    logilcd.Shutdown();
                    return;
                }
                btnInit.Enabled = false;
                btnShutdown.Enabled = true;
                lblStatus.Text = @"[ Initialized ]";
                // force first refresh of screen to avoid waiting before LCD screen is displayed on launch
                RefreshLCDScreen();
                timerUpdate.Start();
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (logilcd == null || logilcd.IsDisposed) return;

            refreshCount += 1;
            if ((refreshCount >= 10 && statsRetriever.CurrentPage == PlanetsideStatsRetriever.PanelPage.KillDeathInfos) ||
                (refreshCount >= 100 && statsRetriever.CurrentPage == PlanetsideStatsRetriever.PanelPage.PlayerInfos))
            {
                RefreshLCDScreen();
                logilcd.Update();
                GetLCDState();
                refreshCount = 0;
            }
            if (logilcd.IsMonoButtonPressed(MONO_BUTTON.BUTTON_0) &&
                statsRetriever.CurrentPage != PlanetsideStatsRetriever.PanelPage.KillDeathInfos)
            {
                statsRetriever.CurrentPage = PlanetsideStatsRetriever.PanelPage.KillDeathInfos;
                RefreshLCDScreen();
                logilcd.Update();
            }
            else if (logilcd.IsMonoButtonPressed(MONO_BUTTON.BUTTON_1) &&
                     statsRetriever.CurrentPage != PlanetsideStatsRetriever.PanelPage.PlayerInfos)
            {
                statsRetriever.CurrentPage = PlanetsideStatsRetriever.PanelPage.PlayerInfos;
                RefreshLCDScreen();
                logilcd.Update();
            }
        }

        private void RefreshLCDScreen()
        {
            if (statsRetriever.Querying == false)
            {
                bool imageChanged = statsRetriever.UpdateStats();
                if (imageChanged)
                {
                    if (checkBoxDisablePreview.Checked == false)
                        pictureBox1.Image = (Bitmap) statsRetriever.FullBitmap.Clone();
                    Marshal.Copy(statsRetriever.BmpData.Scan0, bmpBytes, 0, 6880);
                    Debug.Write("Refreshing screen\n");
                    logilcd.SetMonoBackground(bmpBytes);
                }
            }
        }

        private void GetLCDState()
        {
            if (logilcd.IsConnected(LCD_TYPE.MONO))
            {
                chkMono.Checked = true;
                //chkBtn0.Checked = logilcd.IsMonoButtonPressed( MONO_BUTTON.BUTTON_0 );
                //chkBtn1.Checked = logilcd.IsMonoButtonPressed( MONO_BUTTON.BUTTON_1 );
                //chkBtn2.Checked = logilcd.IsMonoButtonPressed( MONO_BUTTON.BUTTON_2 );
                //chkBtn3.Checked = logilcd.IsMonoButtonPressed( MONO_BUTTON.BUTTON_3 );
            }
            else
            {
                chkMono.Checked = false;
            }

            if (logilcd.IsConnected(LCD_TYPE.COLOR))
            {
                chkColor.Checked = true;
                //chkBtnUp.Checked		= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_UP );
                //chkBtnDown.Checked		= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_DOWN );
                //chkBtnLeft.Checked		= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_LEFT );
                //chkBtnRight.Checked		= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_RIGHT );
                //chkBtnOk.Checked		= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_OK );
                //chkBtnCancel.Checked	= logilcd.IsColorButtonPressed( COLOR_BUTTON.BUTTON_CANCEL );
            }
            else
            {
                chkColor.Checked = false;
            }
        }

        private static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        private void chkMono_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            btnInit.Enabled = true;
            btnShutdown.Enabled = false;

            timerUpdate.Stop();
            logilcd.Shutdown();
            lblStatus.Text = @"[ Not connected ]";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"How to use :
1. Enter your character ID in the text box (You can get it in the URL of your character profile here : https://players.planetside2.com/
2. Press ""Start tracking""
3. Your stats are now being tracked and displayed
4. To stop the tracking, click ""shutdown""");
        }
    }
}