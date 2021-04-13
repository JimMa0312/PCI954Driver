﻿using KGLCtrlApp.entity;
using KGLCtrlApp.userControler;
using KGLCtrlApp.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KGLCtrlApp
{
    public partial class MainWindow : Form
    {
        private List<SwitchCtrl> switchCtrls;
        private WorkSpace workSpace;
        public MainWindow()
        {
            InitializeComponent();
            switchCtrls = new List<SwitchCtrl>();
            InitLastWorkSpace();
            drawSwitchCtrlUI();
        }

        private void LoadWorkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                string filePath;
                WorkSpace tempWorkSpace;
                filePath = openFileDialog.FileName;

                ConfigWorkSpaceUtils configWorkSpaceUtils = new ConfigWorkSpaceUtils();
                tempWorkSpace = configWorkSpaceUtils.LoadWorkSpaceFromFile(filePath);

                if(tempWorkSpace!=null)
                {
                    workSpace = tempWorkSpace;

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    config.AppSettings.Settings["LastWorkSpace"].Value = filePath;
                }
            }
        }

        private void SaveWorkSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
        private bool InitLastWorkSpace()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string filedir = config.AppSettings.Settings["LastWorkSpace"].Value;

            if(string.IsNullOrEmpty(filedir))
            {
                workSpace = null;
                return false;
            }
            else
            {
                return true;
            }
        }

        private void drawSwitchCtrlUI()
        {
            for (int i = 0; i < 16; i++)
            {
                SwitchCtrl tempSwitchCtrl = new SwitchCtrl(i + 1, $"通道{i + 1}");
                SwitchtableLayoutPanel.Controls.Add(tempSwitchCtrl, i % 4, i / 4);

                tempSwitchCtrl.Dock = DockStyle.Fill;
                tempSwitchCtrl.Name = $"switchCtrl{i}";

                switchCtrls.Add(tempSwitchCtrl);
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            LoggerService.IniterLogService();
            LoggerService.logger.Info("启动开关量控制软件");
        }
    }
}
