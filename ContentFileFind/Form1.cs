using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommForAdolph;
using CommForAdolph.QRcode;
using ThoughtWorks.QRCode.Codec;

namespace ContentFileFind
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            QRcode.CreateTGCode("qweqw",50,"E:\\IMG",Color.Black,QrPosition.Middle,0,"E:\\IMG\\打车票.jpg");

            //QRcode.Create("123", 2, "E:\\IMG\\");

        }
    }
}
