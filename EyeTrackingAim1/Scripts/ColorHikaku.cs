using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EyeTrackingAim1.Scripts
{
    public class ColorHikaku
    {
        /// <summary>
        /// 画像をグレースケール化
        /// </summary>
        /// <param name="_img">グレースケール化する画像ファイル</param>
        public Bitmap convertImageGray(Bitmap _img)
        {
            int imgWidth = _img.Width;
            int imgHeight = _img.Height;
            byte[,] data = new byte[imgWidth, imgHeight];

            // bitmapクラスの画像ピクセル値を配列に挿入
            for (int i = 0; i < imgHeight; i++)
            {
                for (int j = 0; j < imgWidth; j++)
                {
                    // グレースケールに変換
                    data[j, i] = (byte)((_img.GetPixel(j, i).R + _img.GetPixel(j, i).B + _img.GetPixel(j, i).G) / 3);
                }
            }

            // 画像データの幅と高さを取得
            int w = data.GetLength(0);
            int h = data.GetLength(1);

            Bitmap resultBmp = new Bitmap(w, h);

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    resultBmp.SetPixel(j, i, Color.FromArgb(data[j, i], data[j, i], data[j, i]));
                }
            }
            return resultBmp;
        }

        /// <summary>
        /// 画像をリサイズ
        /// </summary>
        /// <param name="_img">リサイズする画像ファイル</param>
        /// <param name="_width">リサイズ後の画像の幅</param>
        /// <param name="_height">リサイズ後の画像の高さ</param>
        public Bitmap resizeImage(Bitmap _img, int _width, int _height)
        {
            Bitmap resizeBmp = new Bitmap(_width, _height);
            Graphics g = Graphics.FromImage(resizeBmp);

            // 拡大するときのアルゴリズムの指定
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(_img, 0, 0, _width, _height); // リサイズ
            g.Dispose();
            return resizeBmp;
        }

        /// <summary>
        /// 画像のdHashを計算する
        /// </summary>
        /// <param name="_img">PerceptualHashを計算する画像ファイル</param>
        public string calcPerceptualDhash(Bitmap _img)
        {
            Bitmap grayBmp = convertImageGray(_img); //画像をグレースケール化
            Bitmap resizeBmp = resizeImage(grayBmp, 9, 8); //画像をリサイズ
            string hash = "";
            for (int y = 0; y < resizeBmp.Height; y++)
            {
                for (int x = 0; x < resizeBmp.Width - 1; x++)
                {
                    if (resizeBmp.GetPixel(x, y).R > resizeBmp.GetPixel(x + 1, y).R)
                    {
                        hash = hash + "1";
                    }
                    else
                    {
                        hash = hash + "0";
                    }
                }
            }
            return hash; //64bitの2進数文字列を返す(ex:"00111101101010111010101....")
        }

        /// <summary>
        /// 2つのハッシュ値のハミング距離を求める
        /// </summary>
        /// <param name="_hashA">比較元のハッシュ値(64bit)</param>
        /// <param name="_hashB">比較先のハッシュ値(64bit)</param>
        public int calcHammingDistance(string hashA, string hashB)
        {
            int hammingCount = 0; //ハミング距離計算用の変数
            for (int i = 0; i < hashA.Length; i++)
            {
                if (hashA[i] != hashB[i])
                {
                    // もしビット差分があればカウントを増やす
                    hammingCount++;
                }
            }
            return hammingCount;
        }

        /// <summary>
        /// 2つの画像の差分(ハミング距離)を求める
        /// </summary>
        /// <param name="_imgA">比較する画像A(64bit)</param>
        /// <param name="_imgB">比較する画像B(64bit)</param>
        public int calcHammingDistance(Bitmap _imgA, Bitmap _imgB)
        {
            string dHashA = calcPerceptualDhash(_imgA);
            string dHashB = calcPerceptualDhash(_imgB);
            int hamming = calcHammingDistance(dHashA, dHashB);
            return hamming;
        }
    }
}
