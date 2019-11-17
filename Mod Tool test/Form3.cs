using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Mod_Tool_test
{
    public partial class Form3 : Form
    {
        public String path_real;
        public String path_temp;
        public String path_modlist;
        public String path_modlist_temp;
        public String path_mods;
        public String path_scriptextender;
        public String path_plugins;
        public String path_plugins_temp;
        public String path_plugins_temp2;

        public String xEditPath;
        public String LootPath;
        public String iniPath;
        public String f4sePath;
        public String xLODGenPath;


        public Form3()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Skyrim SE
            path_real = "D:/Program Files (x86)/Steam/steamapps/common/Skyrim Special Edition/Data";
            path_temp = "D:/Mod Tools/Mod tool test/Temporario";
            path_modlist = "D:/Simple Mod Tool/Skyrim SE/modlist.txt";
            path_modlist_temp = "D:/Simple Mod Tool/Skyrim SE/modlist2.txt";
            path_mods = "D:/Simple Mod Tool/Skyrim SE/mods";          
            path_plugins = "C:/Users/Inu/AppData/Local/Skyrim Special Edition/plugins.txt";
            path_plugins_temp = "D:/Simple Mod Tool/Skyrim SE/plugins3.txt";
            path_plugins_temp2 = "D:/Simple Mod Tool/Skyrim SE/plugins2.txt";
            xEditPath = "D:/Mod Tools/SSEdit/SSEEdit.exe";
            LootPath = "D:/Mod Tools/LOOT/LOOT.exe";
            iniPath = "D:/Documents/My Games/Skyrim Special Edition/SkyrimCustom.ini";
            f4sePath = "D:/Program Files (x86)/Steam/steamapps/common/Skyrim Special Edition/skse64_loader.exe";

            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Fallout 4
            path_real = "D:/Program Files (x86)/Steam/steamapps/common/Fallout 4/Data";
            path_temp = "D:/Mod Tools/Mod tool test/Temporario";
            path_modlist = "D:/Simple Mod Tool/Fallout 4/modlist.txt";
            path_modlist_temp = "D:/Simple Mod Tool/Fallout 4/modlist2.txt";
            path_mods = "D:/Simple Mod Tool/Fallout 4/mods";
            path_scriptextender = "D:/Mod Tools/Mod tool test/Real";
            path_plugins = "C:/Users/Inu/AppData/Local/Fallout4/plugins.txt";
            path_plugins_temp = "D:/Simple Mod Tool/Fallout 4/plugins3.txt";
            path_plugins_temp2 = "D:/Simple Mod Tool/Fallout 4/plugins2.txt";
            xEditPath = "D:/Simple Mod Tool/FO4Edit/FO4Edit64.exe";
            LootPath = "D:/Simple Mod Tool/LOOT/LOOT.exe";
            iniPath = "D:/Documents/My Games/Fallout4/Fallout4Custom.ini";
            f4sePath = "D:/Program Files (x86)/Steam/steamapps/common/Fallout 4/f4se_loader.exe";
            xLODGenPath = "D:/Simple Mod Tool/xLODGen 34/FO4LODGenx64.exe - Atalho";

            Hide();
        }
    }
}
