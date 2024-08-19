using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using EyeTrackingAim1.Scripts.Calibration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EyeTrackingAim1.Scripts.python
{
    public class do_python
    {
        public class betas
        {
            [JsonPropertyName("beta")] public double[] beta { get; set; } = new double[1];
        }
        //Thread
        public static Thread python_thread;

        public static void test()
        {
            python_thread = new Thread(new ThreadStart(() =>
            {
                

                //下記のPythonスクリプトへのファイルパスを記述する
                string myPythonApp = "keisan_pyfile/tuning/test.py";

                
                var myProcess = new Process
                {
                    StartInfo = new ProcessStartInfo("keisan_pyfile/tuning/python_bat_test.bat")
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Arguments = myPythonApp,
                        CreateNoWindow = true
                    }
                };

                myProcess.Start();
                StreamReader myStreamReader = myProcess.StandardOutput;
                string myString = myStreamReader.ReadLine();
                myProcess.WaitForExit();
                myProcess.Close();

                Console.WriteLine(myString);
                betas betas_ = JsonSerializer.Deserialize<betas>(myString);
                //CalibrationClass.cali_Manual_offset.fit_offset_x = betas_.beta;

                //Console.WriteLine("Value received from script: " + CalibrationClass.cali_Manual_offset.fit_offset_x[0]);


                python_thread.Abort();
            }));

            python_thread.Start();
        }
    }
}
