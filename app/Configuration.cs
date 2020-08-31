using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;

namespace app
{

    #region CustomSection class

    public class CustomSection : ConfigurationSection
    {

        public CustomSection()
        {
        }

        [ConfigurationProperty("port", DefaultValue = "COM3", IsRequired = true, IsKey = true)]
        public string Port
        {
            get
            {
                return (string)this["port"];
            }
            set
            {
                this["port"] = value;
            }
        }


        [ConfigurationProperty("period", DefaultValue = 100, IsRequired = true, IsKey = true)]
        public int Period
        {
            get
            {
                return (int)this["period"];
            }
            set
            {
                this["period"] = value;
            }
        }

    }

    #endregion



    #region Configuration Class
    public class AppConfiguration
    {


        public static CustomSection GetCustomSection()
        {
            try
            {

                CustomSection customSection;


                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                customSection = config.GetSection("CustomSection") as CustomSection;

                if (customSection == null)
                {
                    customSection = new CustomSection();
                }

                Console.WriteLine("COM Port: {0}", customSection.Port);
                Console.WriteLine("Period: {0}", customSection.Period);

                return customSection;
            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("Using GetSection(string): {0}", err.ToString());
            }
            return null;
        }



        public static void SaveConfig(CustomSection customSection)
        {
            try
            {


                System.Configuration.Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None) as Configuration;

                if (config.GetSection("CustomSection") == null)
                {
                    config.Sections.Add("CustomSection", customSection);
                }
                else
                {
                    var section = config.GetSection("CustomSection") as CustomSection;
                    section.Port = customSection.Port;
                }


                config.Save();


            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("Using SaveConfig(CustomSection): {0}", err.ToString());
            }

        }



    }
    #endregion



}