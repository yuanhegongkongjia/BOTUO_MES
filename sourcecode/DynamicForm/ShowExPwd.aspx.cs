using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DynamicForm
{
    public partial class ShowExPwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var gif = CreateRandomCode(4);
            Session["gif"] = gif;
            CreateImage(Session["gif"].ToString());

        }

        /// <summary>
        /// 这个是使用字母,数字混合
        /// </summary>
        /// <param name="VcodeNum"></param>
        /// <returns></returns>
        private string CreateRandomCode(int codeCount)
        {
            //string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            string allChar = "2,3,4,5,6,7,8,9,a,d,f,g,h,z,c,v,b,n,m,k,q,w,e,r,t,y,u,p,A,S,D,F,G,H,Z,C,V,B,N,M,K,Q,W,E,R,T,Y,U,P"; //定义验证码字符及出现频次 ,避免出现0 o j i l 1 x;  
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(49);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        private void CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 20);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(Color.Black);
            g.FillRectangle(new System.Drawing.SolidBrush(Color.White), 0, 0, image.Width, image.Height);   
            //g.Clear(Color.Gray);

            //画曲线
            //g.DrawImageUnscaled(DrawRandomBezier(2,image.Width,image.Height), 0, 0);
            //画直线
            g.DrawImageUnscaled(DrawRandomLine(1, image.Width, image.Height), 0, 0);


            //g.DrawString(checkCode, f, b, 3, 3);
            DrawRandomString(image, checkCode,image.Width, image.Height);
            Pen blackPen = new Pen(Color.Black, 0);
            Random rand = new Random();
  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Png";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }

        #region 随机生成贝塞尔曲线
        /// <summary>
        /// 随机生成贝塞尔曲线
        /// </summary>
        /// <param name="bmp">一个图片的实例</param>
        /// <param name="lineNum">线条数量</param>
        /// <returns></returns>
        public Bitmap DrawRandomBezier(Int32 lineNum, int bgWidth, int bgHeight)
        {
            Bitmap b = new Bitmap(bgWidth, bgHeight);
            b.MakeTransparent();
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            GraphicsPath gPath1 = new GraphicsPath();
            Random random = new Random();
            Int32 lineRandNum = random.Next(lineNum);

            for (int i = 0; i < (lineNum - lineRandNum); i++)
            {
                Pen p = new Pen(GetRandomDeepColor());
                Point[] point = {
                                    new Point(random.Next(1, (b.Width / 10)), random.Next(1, (b.Height))),
                                    new Point(random.Next((b.Width / 10) * 2, (b.Width / 10) * 4), random.Next(1, (b.Height))),
                                    new Point(random.Next((b.Width / 10) * 4, (b.Width / 10) * 6), random.Next(1, (b.Height))),
                                    new Point(random.Next((b.Width / 10) * 8, b.Width), random.Next(1, (b.Height)))
                                };

                gPath1.AddBeziers(point);
                g.DrawPath(p, gPath1);
                p.Dispose();
            }
            for (int i = 0; i < lineRandNum; i++)
            {
                Pen p = new Pen(GetRandomDeepColor());
                Point[] point = {
                            new Point(random.Next(1, b.Width), random.Next(1, b.Height)),
                            new Point(random.Next((b.Width / 10) * 2, b.Width), random.Next(1, b.Height)),
                            new Point(random.Next((b.Width / 10) * 4, b.Width), random.Next(1, b.Height)),
                            new Point(random.Next(1, b.Width), random.Next(1, b.Height))
                                };
                gPath1.AddBeziers(point);
                g.DrawPath(p, gPath1);
                p.Dispose();
            }
            return b;
        }
        #endregion

        #region 画直线
        /// <summary>
        /// 画直线
        /// </summary>
        /// <param name="bmp">一个bmp实例</param>
        /// <param name="lineNum">线条个数</param>
        /// <returns></returns>
        public Bitmap DrawRandomLine(Int32 lineNum, int bgWidth, int bgHeight)
        {
            if (lineNum < 0) throw new ArgumentNullException("参数bmp为空！");
            Bitmap b = new Bitmap(bgWidth, bgHeight);
            b.MakeTransparent();
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Transparent);
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            var random = new Random();
            for (int i = 0; i < lineNum; i++)
            {
                Pen p = new Pen(GetRandomDeepColor());
                Point pt1 = new Point(random.Next(1, (b.Width / 5) * 2), random.Next(b.Height));
                Point pt2 = new Point(random.Next((b.Width / 5) * 3, b.Width), random.Next(b.Height));
                g.DrawLine(p, pt1, pt2);
                p.Dispose();
            }

            return b;
        }
        #endregion

        #region 生成随机颜色
        /// <summary>
        /// 生成随机深颜色
        /// </summary>
        /// <returns></returns>
        public Color GetRandomDeepColor()
        {
            int nRed, nGreen, nBlue;    // nBlue,nRed  nGreen 相差大一点 nGreen 小一些
            //int high = 255;       
            int redLow = 160;
            int greenLow = 100;
            int blueLow = 160;
            var random = new Random();
            nRed = random.Next(redLow);
            nGreen = random.Next(greenLow);
            nBlue = random.Next(blueLow);
            Color color = Color.FromArgb(nRed, nGreen, nBlue);
            return color;
        }
        #endregion

        #region
        /// <summary>
        /// 写入验证码的字符串
        /// </summary>
        private void DrawRandomString(Bitmap b,string code,int bgWidth,int bgHeight)
        {
            
            //b.MakeTransparent();
            Graphics g = Graphics.FromImage(b);

            //g.Clear(Color.White);
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            char[] chars = code.ToCharArray();//拆散字符串成单字符数组
            var validationCode = chars.ToString();
            var validationCodeCount=chars.Length;
            var random=new Random();
            //设置字体显示格式
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            FontFamily f = new FontFamily(GenericFontFamilies.Monospace);


            Int32 charNum = chars.Length;

            Point sPoint = new Point();
            Int32 fontSize = 12;
            for (int i = 0; i < validationCodeCount; i++)
            {
                int findex = random.Next(5);
                //定义字体
                Font textFont = new Font(f, random.Next(15, 15), FontStyle.Bold);
                //Font textFont = new System.Drawing.Font("Arial", random.Next(10, 15), System.Drawing.FontStyle.Bold);
                //定义画刷，用于写字符串
                //Brush brush = new SolidBrush(GetRandomDeepColor());
                Int32 textFontSize = Convert.ToInt32(textFont.Size);
                fontSize = textFontSize;
                Point point = new Point(random.Next((bgWidth / charNum) * i + 5, (bgWidth / charNum) * (i + 1)), random.Next(bgHeight / 5 + textFontSize / 2, bgHeight - textFontSize / 2));

                var strPoint = new Point[validationCodeCount + 1];

                //如果当前字符X坐标小于字体的二分之一大小
                if (point.X < textFontSize / 2)
                {
                    point.X = point.X + textFontSize / 2;
                }
                //防止文字叠加
                if (i > 0 && (point.X - sPoint.X < (textFontSize / 2 + textFontSize / 2)))
                {
                    point.X = point.X + textFontSize;
                }
                //如果当前字符X坐标大于图片宽度，就减去字体的宽度
                if (point.X > (bgWidth - textFontSize / 2))
                {
                    point.X = bgWidth - textFontSize / 2;
                }

                sPoint = point;

                float angle = random.Next(-40, 40);//转动的度数
                g.TranslateTransform(point.X, point.Y);//移动光标到指定位置
                g.RotateTransform(angle);

                //设置渐变画刷  
                Rectangle myretang = new Rectangle(0, 1, Convert.ToInt32(textFont.Size), Convert.ToInt32(textFont.Size));
                Color c = GetRandomDeepColor();
                LinearGradientBrush mybrush2 = new LinearGradientBrush(myretang, c, GetLightColor(c, 120), random.Next(180));

                g.DrawString(chars[i].ToString(), textFont, mybrush2, 1, 1, format);

                g.RotateTransform(-angle);//转回去
                g.TranslateTransform(-point.X, -point.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别

                strPoint[i] = point;

                textFont.Dispose();
                mybrush2.Dispose();
            }
        }
        #endregion

        #region
        /// <summary>
        /// 获取与当前颜色值相加后的颜色
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Color GetLightColor(Color c, Int32 value)
        {
            int nRed = c.R, nGreen = c.G, nBlue = c.B;    //越大颜色越浅
            if (nRed + value < 255 && nRed + value > 0)
            {
                nRed = c.R + 40;
            }
            if (nGreen + value < 255 && nGreen + value > 0)
            {
                nGreen = c.G + 40;
            }
            if (nBlue + value < 255 && nBlue + value > 0)
            {
                nBlue = c.B + 40;
            }
            Color color = Color.FromArgb(nRed, nGreen, nBlue);
            return color;
        }
        #endregion
    }
}