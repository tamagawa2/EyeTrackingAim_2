using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EyeTrackingAim1.Scripts.Calibration;

namespace EyeTrackingAim1.Scripts.Fine_tunig
{
    public class set_fine_tunig_data
    {
        int f8_state = 0;
        int ikkai_size;
        int cali_size;
        int ikkoatari;
        public CalibrationClass.CalibrationDataHozon cali_co_fine_tuning;
        public System.Windows.Vector[] cali_co_vec;
        public int[] index_f;

        public void Init_set_fine_tunig_data()
        {
            ikkai_size = 0;
            cali_size = 1 + ikkai_size * 2;
            ikkoatari = 1;

            cali_co_fine_tuning = new CalibrationClass.CalibrationDataHozon(cali_size * ikkoatari);
            cali_co_vec = new System.Windows.Vector[cali_size];
            index_f = new int[cali_size];
            int warucount = 0;
            int index = 0;
            
            for (int i = 0; i < cali_size; i++)
            {
                if (i == 0)
                {
                    cali_co_vec[i].X = 960.0;
                    cali_co_vec[i].Y = 540.0;
                }
                else
                {
                    if ((i - 1) % ikkai_size == 0)
                    {
                        warucount = warucount + 1;
                        index = 0;
                    }
                    double yoko_first = CalibrationClass.yoko_l / Math.Pow(2.0, warucount);
                    double tate_first = CalibrationClass.tate_l / Math.Pow(2.0, warucount);
                    //毎回の長さ
                    double yoko_maikai = CalibrationClass.yoko_l / Math.Pow(2.0, warucount - 1);
                    double tate_maikai = CalibrationClass.tate_l / Math.Pow(2.0, warucount - 1);

                    //インデックス
                    int yoko_index = index % (int)Math.Sqrt(ikkai_size);
                    int tate_index = index / (int)Math.Sqrt(ikkai_size);

                    index = index + 1;

                    cali_co_vec[i].X = 960.0 - yoko_first + yoko_maikai * yoko_index;
                    cali_co_vec[i].Y = 540.0 - tate_first + tate_maikai * tate_index;


                }

                for (int j = 0; j < ikkoatari; j++)
                {
                    cali_co_fine_tuning.CalibrationTarget[i * ikkoatari + j] = cali_co_vec[i];
                }

            }
        }

        public void set_fine_tuning(bool f8_flag, System.Windows.Vector eye_pos)
        {
            if (f8_flag == true)
            {
                for (int i = 0; i < cali_size; i++)
                {
                    if ((cali_co_vec[i] - eye_pos).Length < 40.0)
                    {

                        cali_co_fine_tuning.CalibrationPointArray[i * ikkoatari + index_f[i]] = eye_pos - cali_co_vec[i];
                        cali_co_fine_tuning.CalibrationEyeArray[i * ikkoatari + index_f[i]] = eye_pos;
                        index_f[i] = (index_f[i] + 1) % ikkoatari;

                    }
                }
            }

            
            if (f8_state == 0 && f8_flag == true)
            {                
                f8_state = 1;
            }
            else if (f8_state == 1 && f8_flag == false)
            {
                cali_co_fine_tuning.bunnkatu = 1;
                RecordCalibration.RecoldCaliData(cali_co_fine_tuning, "EyeData/CalibrationData/Cali_Co_Fine/Data.bin");
                f8_state = 0;
            }

        }


        
    }
}
