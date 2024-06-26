﻿using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp.Modules.Template
{
    public partial class ReportView : UserControl
    {
        public ReportView()
        {
            InitializeComponent();
        }

        public CrystalReportViewer CrystalReportViewer
        {
            get
            {
                return crReportView;
            }
            set
            {
                crReportView = value;
            }
        }
    }
}
