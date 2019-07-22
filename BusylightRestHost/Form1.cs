using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bl_host
{
    public partial class Form1 : Form
    {
//        internal  BusylightController busylight = new BusylightController();
        
        public Form1()
        {
            InitializeComponent();
        //    String Json = @"{""action"":""color"",""parameters"":[{""Key"":""color"",""Value"":""#ff0000""}]}";
          //  busylight.runAction(BusylightAction.FromJson(Json));
          var httpServer = new HttpServer();
          httpServer.start();
        }
    }
}