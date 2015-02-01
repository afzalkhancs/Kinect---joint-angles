using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Globalization;


namespace Play_v_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StreamWriter writer;

        public DateTime Time { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            writer = new StreamWriter("myfile.txt", true);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor oldsensor = (KinectSensor)e.OldValue;

            StopKinect(oldsensor);

            KinectSensor newsensor = (KinectSensor)e.NewValue;

            newsensor.DepthStream.Enable();
            newsensor.SkeletonStream.Enable();
           // newsensor.ColorStream.Enable();


            //newsensor.ColorFrameReady +=new EventHandler<ColorImageFrameReadyEventArgs>(newsensor_ColorFrameReady);
            newsensor.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(newsensor_SkeletonFrameReady);
            //newsensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(newsensor_AllFramesReady);
            newsensor.Start();
            //throw new NotImplementedException();
        }

        


       /* public void SetEllipsePosition(Ellipse ellipse, Joint joint)
        {
            Canvas.SetLeft(ellipse, (320 * joint.Position.X) + 320);
            Canvas.SetTop(ellipse, (240 * -joint.Position.Y) + 240);

        }*/

        

        Skeleton[] skeletons = null;


        void newsensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    if (this.skeletons == null)
                    {
                        this.skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    }
                    skeletonFrame.CopySkeletonDataTo(this.skeletons);
                    Skeleton skeleton = this.skeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked).FirstOrDefault();

                    /*// saving the images captured
                      RenderTargetBitmap bmp = new RenderTargetBitmap(800, 600, 96, 96, PixelFormats.Pbgra32);
                        //bmp.Render(window.image);

                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                        // create frame from the writable bitmap and add to encoder

                        encoder.Frames.Add(BitmapFrame.Create(bmp));
                        string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                        string path = skeletonFrame.Timestamp + ".jpg";
                        using (FileStream fs = new FileStream(path, FileMode.Create))
                        {
                            encoder.Save(fs);
                            if (fs.Length > 1024)
                            {
                                fs.Close();

                            }
                        
                        }*/


                    /* if (skeleton != null)
                     {
                         // Calculate height.
                         double height = Math.Round(skeleton.Height(), 2);

                         // Draw skeleton joints.
                         foreach (JointType joint in Enum.GetValues(typeof(JointType)))
                         {
                             DrawJoint(skeleton.Joints[joint].ScaleTo(640, 480));
                         }

                         // Display height.
                         tblHeight.Text = "Height: " + height.ToString() + "m";



                     */

                    /*if (skeleton != null)
                     {
                         // Calculate height.
                       //  double height = Math.Round(skeleton.Height(), 2);

                     }*/

                    /*

                    if (skeleton != null)
                    {
                        // DrawingContext drawing;

                        SetEllipsePosition(HipCenter, skeleton.Joints[JointType.HipCenter]);

                        //DrawText(new FormattedText("Hipcenter", CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 12, System.Windows.Media.Brushes.Black), new System.Windows.Point(200, 116));

                        SetEllipsePosition(Spine, skeleton.Joints[JointType.Spine]);

                        SetEllipsePosition(ShoulderCenter, skeleton.Joints[JointType.ShoulderCenter]);
                        SetEllipsePosition(Head, skeleton.Joints[JointType.Head]);
                        SetEllipsePosition(ShoulderLeft, skeleton.Joints[JointType.ShoulderLeft]);
                        SetEllipsePosition(ElbowLeft, skeleton.Joints[JointType.ElbowLeft]);
                        SetEllipsePosition(WristLeft, skeleton.Joints[JointType.WristLeft]);
                        SetEllipsePosition(HandLeft, skeleton.Joints[JointType.HandLeft]);
                        SetEllipsePosition(ShoulderRight, skeleton.Joints[JointType.ShoulderRight]);
                        SetEllipsePosition(ElbowRight, skeleton.Joints[JointType.ElbowRight]);
                        SetEllipsePosition(WristRight, skeleton.Joints[JointType.WristRight]);
                        SetEllipsePosition(HandRight, skeleton.Joints[JointType.HandRight]);
                        SetEllipsePosition(HipLeft, skeleton.Joints[JointType.HipLeft]);
                        SetEllipsePosition(KneeLeft, skeleton.Joints[JointType.KneeLeft]);
                        SetEllipsePosition(AnkleLeft, skeleton.Joints[JointType.AnkleLeft]);
                        SetEllipsePosition(FootLeft, skeleton.Joints[JointType.FootLeft]);
                        SetEllipsePosition(HipRight, skeleton.Joints[JointType.HipRight]);
                        SetEllipsePosition(KneeRight, skeleton.Joints[JointType.KneeRight]);
                        SetEllipsePosition(AnkleRight, skeleton.Joints[JointType.AnkleRight]);
                        SetEllipsePosition(FootRight, skeleton.Joints[JointType.FootRight]);


                    }
                    */



                    if (skeleton != null)
                    {
                        Joint hipcenter = skeleton.Joints[JointType.HipCenter];
                        Joint hipright = skeleton.Joints[JointType.HipRight];
                        Joint kneeright = skeleton.Joints[JointType.KneeRight];
                        Joint ankleright = skeleton.Joints[JointType.AnkleRight];
                        Joint footright = skeleton.Joints[JointType.FootRight];
                        Joint hipleft = skeleton.Joints[JointType.HipLeft];
                        Joint kneeleft = skeleton.Joints[JointType.KneeLeft];
                        Joint ankleleft = skeleton.Joints[JointType.AnkleLeft];
                        Joint footleft = skeleton.Joints[JointType.FootLeft];

                        Time = DateTime.Now;

                        //Joint j2 = skeleton.Joints[JointType.KneeLeft];
                        //writer.Writeline("X Y z");
                        /*

                        if (hipcenter.TrackingState == JointTrackingState.Tracked )//|| hipcenter.TrackingState== JointTrackingState.Inferred || hipcenter.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("hipcenter:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                        else

                            
                            writer.Write("hipcenter:\t NA \t NA \t NA \t");
                        
                        if (hipright.TrackingState == JointTrackingState.Tracked)// || hipright.TrackingState == JointTrackingState.Inferred || hipright.TrackingState==JointTrackingState.NotTracked)

                            writer.Write("hipright:\t " + hipright.Position.X + "\t" + hipright.Position.Y + "\t" + hipright.Position.Z + "\t");
                        else

                            writer.Write("hipright:\t NA \t NA \t NA \t");


                        if (kneeright.TrackingState == JointTrackingState.Tracked )//|| kneeright.TrackingState==JointTrackingState.Inferred || kneeright.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("kneeright:\t " + kneeright.Position.X + "\t" + kneeright.Position.Y + "\t" + kneeright.Position.Z + "\t");
                        else

                            writer.Write("kneeright:\t NA \t NA \t NA \t");
                       
                        
                        if (ankleright.TrackingState == JointTrackingState.Tracked )//|| ankleright.TrackingState==JointTrackingState.Inferred || ankleright.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("ankleright:\t " + ankleright.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleright.Position.Z + "\t");
                        else

                            writer.Write("ankleright:\t NA \t NA \t NA \t");

                        
                        if (footright.TrackingState == JointTrackingState.Tracked )//|| footright.TrackingState == JointTrackingState.Inferred || footright.TrackingState ==JointTrackingState.NotTracked)

                            writer.Write("footright:\t " + footright.Position.X + "\t" + footright.Position.Y + "\t" + footright.Position.Z + "\t");
                        else

                            writer.Write("footright:\t NA \t NA \t NA \t");

                        
                       
                        if (hipleft.TrackingState == JointTrackingState.Tracked )//|| hipleft.TrackingState == JointTrackingState.Inferred || hipright.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("hip left:\t " + hipleft.Position.X + "\t" + hipleft.Position.Y + "\t" + hipleft.Position.Z + "\t");
                        else

                            writer.Write("hipleft:\t NA \t NA \t NA \t");


                        if (kneeleft.TrackingState == JointTrackingState.Tracked )//|| kneeleft.TrackingState==JointTrackingState.Inferred || kneeleft.TrackingState== JointTrackingState.NotTracked )

                            writer.Write("knee left:\t " + kneeleft.Position.X + "\t" + kneeleft.Position.Y + "\t" + kneeleft.Position.Z + "\t");
                        else

                            writer.Write("kneeleft:\t NA \t NA \t NA \t ");

                        if (ankleleft.TrackingState == JointTrackingState.Tracked )//|| ankleleft.TrackingState == JointTrackingState.Inferred || ankleleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("ankleleft:\t " + ankleleft.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleleft.Position.Z + "\t");
                        else

                            writer.Write("ankleleft:\t NA \t NA \t NA \t ");


                        if (footleft.TrackingState == JointTrackingState.Tracked)// || footleft.TrackingState == JointTrackingState.Inferred || footleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("footleft:\t " + footleft.Position.X + "\t" + footleft.Position.Y + "\t" + footleft.Position.Z + "\t");
                        else

                            writer.Write("footleft:\t NA \t NA \t NA \t ");


                        writer.WriteLine("TIME:\t" + DateTime.Now + "\t"); */

                        
                        
                        if (hipcenter.TrackingState == JointTrackingState.Tracked)
                            writer.Write("hipcenter:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                            else
                                if (hipcenter.TrackingState == JointTrackingState.Inferred)
                                    writer.Write("hipcenter/INF/:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                        
                                        else 
                                            
                                            if (hipcenter.TrackingState == JointTrackingState.NotTracked)
                                                 writer.Write("hipcenter/[NT/]:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                                                     else

                                                       writer.Write("hipcenter/["+hipcenter.TrackingState+"/]:\t NA \t NA \t NA ");

                         if (hipright.TrackingState == JointTrackingState.Tracked)
                            writer.Write("hipright:\t " + hipright.Position.X + "\t" + hipright.Position.Y + "\t" + hipright.Position.Z + "\t ");
                            else
                                if (hipright.TrackingState == JointTrackingState.Inferred)
                                    writer.Write("hipright/INF/:\t " + hipright.Position.X + "\t" + hipright.Position.Y + "\t" + hipright.Position.Z + "\t ");
                        
                                        else 
                                            
                                            if (hipright.TrackingState == JointTrackingState.NotTracked)
                                                 writer.Write("hipright/NT/:\t " + hipright.Position.X + "\t" + hipright.Position.Y + "\t" + hipright.Position.Z + "\t ");
                                                     else

                                                       writer.Write("hipright/["+hipright.TrackingState+"/]:\t NA \t NA \t NA ");

                         if (kneeright.TrackingState == JointTrackingState.Tracked)
                             writer.Write("kneeright:\t " + kneeright.Position.X + "\t" + kneeright.Position.Y + "\t" + kneeright.Position.Z + "\t ");
                         else
                             if (kneeright.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("kneeright/INF/:\t " + kneeright.Position.X + "\t" + kneeright.Position.Y + "\t" + kneeright.Position.Z + "\t ");

                             else

                                 if (kneeright.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("kneeright/NT/:\t " + kneeright.Position.X + "\t" + kneeright.Position.Y + "\t" + kneeright.Position.Z + "\t ");
                                 else

                                     writer.Write("kneeright/[" + kneeright.TrackingState + "/]:\t NA \t NA \t NA ");

                         if (ankleright.TrackingState == JointTrackingState.Tracked)
                             writer.Write("ankleright:\t " + ankleright.Position.X + "\t" + ankleright.Position.Y + "\t" + ankleright.Position.Z + "\t ");
                         else
                             if (ankleright.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("ankleright/INF/:\t " + ankleright.Position.X + "\t" + ankleright.Position.Y + "\t" + ankleright.Position.Z + "\t ");

                             else

                                 if (ankleright.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("ankleright/NT/:\t " + ankleright.Position.X + "\t" + ankleright.Position.Y + "\t" + ankleright.Position.Z + "\t ");
                                 else

                                     writer.Write("ankleright/[" + ankleright.TrackingState + "/]:\t NA \t NA \t NA ");


                         if (footright.TrackingState == JointTrackingState.Tracked)
                             writer.Write("footright:\t " + footright.Position.X + "\t" + footright.Position.Y + "\t" + footright.Position.Z + "\t ");
                         else
                             if (footright.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("footright/INF/:\t " + footright.Position.X + "\t" + footright.Position.Y + "\t" + footright.Position.Z + "\t ");

                             else

                                 if (footright.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("footright/NT/:\t " + footright.Position.X + "\t" + footright.Position.Y + "\t" + footright.Position.Z + "\t ");
                                 else

                                     writer.Write("footright/[" + footright.TrackingState + "/]:\t NA \t NA \t NA ");
/****************************************************************************************************/
                        // for left side
                         if (hipcenter.TrackingState == JointTrackingState.Tracked)
                             writer.Write("hipcenter:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                         else
                             if (hipcenter.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("hipcenter/INF/:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");

                             else

                                 if (hipcenter.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("hipcenter/[NT/]:\t " + hipcenter.Position.X + "\t" + hipcenter.Position.Y + "\t" + hipcenter.Position.Z + "\t ");
                                 else

                                     writer.Write("hipcenter/[" + hipcenter.TrackingState + "/]:\t NA \t NA \t NA ");



                         if (hipleft.TrackingState == JointTrackingState.Tracked)
                             writer.Write("hipleft:\t " + hipleft.Position.X + "\t" + hipleft.Position.Y + "\t" + hipleft.Position.Z + "\t ");
                         else
                             if (hipleft.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("hipleft/INF/:\t " + hipleft.Position.X + "\t" + hipleft.Position.Y + "\t" + hipleft.Position.Z + "\t ");

                             else

                                 if (hipleft.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("hipleft/NT/:\t " + hipleft.Position.X + "\t" + hipleft.Position.Y + "\t" + hipleft.Position.Z + "\t ");
                                 else

                                     writer.Write("hipleft/[" + hipleft.TrackingState + "/]:\t NA \t NA \t NA ");

                        
                        
                        if (kneeleft.TrackingState == JointTrackingState.Tracked)
                             writer.Write("kneeleft:\t " + kneeleft.Position.X + "\t" + kneeleft.Position.Y + "\t" + kneeleft.Position.Z + "\t ");
                         else
                             if (kneeleft.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("kneeleft/INF/:\t " + kneeleft.Position.X + "\t" + kneeleft.Position.Y + "\t" + kneeleft.Position.Z + "\t ");

                             else

                                 if (kneeleft.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("kneeleft/NT/:\t " + kneeleft.Position.X + "\t" + kneeleft.Position.Y + "\t" + kneeleft.Position.Z + "\t ");
                                 else

                                     writer.Write("kneeleft/[" + kneeleft.TrackingState + "/]:\t NA \t NA \t NA ");


                         if (ankleleft.TrackingState == JointTrackingState.Tracked)
                             writer.Write("ankleleft:\t " + ankleleft.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleleft.Position.Z + "\t ");
                         else
                             if (ankleleft.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("ankleleft/INF/:\t " + ankleleft.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleleft.Position.Z + "\t ");

                             else

                                 if (ankleleft.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("ankleleft/NT/:\t " + ankleleft.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleleft.Position.Z + "\t ");
                                 else

                                     writer.Write("ankleleft/[" + ankleleft.TrackingState + "/]:\t NA \t NA \t NA ");


                         if (footleft.TrackingState == JointTrackingState.Tracked)
                             writer.Write("footleft:\t " + footleft.Position.X + "\t" + footleft.Position.Y + "\t" + footleft.Position.Z + "\t ");
                         else
                             if (footleft.TrackingState == JointTrackingState.Inferred)
                                 writer.Write("footleft/INF/:\t " + footleft.Position.X + "\t" + footleft.Position.Y + "\t" + footleft.Position.Z + "\t ");

                             else

                                 if (footleft.TrackingState == JointTrackingState.NotTracked)
                                     writer.Write("footleft/NT/:\t " + footleft.Position.X + "\t" + footleft.Position.Y + "\t" + footleft.Position.Z + "\t ");
                                 else

                                     writer.Write("footleft/[" + footleft.TrackingState + "/]:\t NA \t NA \t NA ");


                        /*
                     
                        if (hipleft.TrackingState == JointTrackingState.Tracked || hipleft.TrackingState == JointTrackingState.Inferred || hipleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("hip left:\t " + hipleft.Position.X + "\t" + hipleft.Position.Y + "\t" + hipleft.Position.Z + "\t");
                        else

                            writer.Write("hipleft:\t NA \t NA \t NA ");


                        if (kneeleft.TrackingState == JointTrackingState.Tracked || kneeleft.TrackingState == JointTrackingState.Inferred || kneeleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("knee left:\t " + kneeleft.Position.X + "\t" + kneeleft.Position.Y + "\t" + kneeleft.Position.Z + "\t");
                        else

                            writer.Write("kneeleft:\t NA \t NA \t NA ");

                        if (ankleleft.TrackingState == JointTrackingState.Tracked || ankleleft.TrackingState == JointTrackingState.Inferred || ankleleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("ankleleft:\t " + ankleleft.Position.X + "\t" + ankleleft.Position.Y + "\t" + ankleleft.Position.Z + "\t");
                        else

                            writer.Write("ankleleft:\t NA \t NA \t NA ");


                        if (footleft.TrackingState == JointTrackingState.Tracked || footleft.TrackingState == JointTrackingState.Inferred || footleft.TrackingState == JointTrackingState.NotTracked)

                            writer.Write("footleft:\t " + footleft.Position.X + "\t" + footleft.Position.Y + "\t" + footleft.Position.Z + "\t");
                        else

                            writer.Write("footleft:\t NA \t NA \t NA ");*/


                        writer.WriteLine("TIME:\t" + DateTime.Now + "\t");

                        

                        /*
                        if (hipcenter.TrackingState == JointTrackingState.Tracked
                            && hipright.TrackingState == JointTrackingState.Tracked
                            && kneeright.TrackingState == JointTrackingState.Tracked
                            && ankleright.TrackingState == JointTrackingState.Tracked
                            && footright.TrackingState == JointTrackingState.Tracked
                            && hipleft.TrackingState == JointTrackingState.Tracked
                            && kneeleft.TrackingState == JointTrackingState.Tracked
                            && ankleleft.TrackingState == JointTrackingState.Tracked
                            && footleft.TrackingState == JointTrackingState.Tracked)*/
                       
                        /*{


                            //StreamWriter writer = new StreamWriter("myfile.txt", false);


                            //writer.Write();

                            //Console.WriteLine("The output of X Y Z coordinates");



                            writer.WriteLine("hipcenter: " + hipcenter.Position.X + "," + hipcenter.Position.Y + "," + hipcenter.Position.Z + "\t"
                           + "hipright: " + hipright.Position.X + "," + hipright.Position.Y + "," + hipright.Position.Z + "\t"
                            + "kneeright: " + kneeright.Position.X + "," + kneeright.Position.Y + "," + kneeright.Position.Z +"\t"
                            + "ankleright: " + ankleright.Position.X + "," + ankleright.Position.Y + "," + ankleright.Position.Z +"\t"
                           + "footright: " + footright.Position.X + "," + footright.Position.Y + "," + footright.Position.Z + "\t"
                           +"hipleft: " + hipleft.Position.X + "," + hipleft.Position.Y + "," + hipleft.Position.Z +"\t"
                           + "kneeleftleft: " + kneeleft.Position.X + "," + kneeleft.Position.Y + "," + kneeleft.Position.Z + "\t"
                           + "ankleleft: " + ankleleft.Position.X + "," + ankleleft.Position.Y + "," + ankleleft.Position.Z +"\t"
                           + "footleft: " + footleft.Position.X + "," + footleft.Position.Y + "," + footleft.Position.Z +"\t"
                           +"TIME" + DateTime.Now);
                            //writer.WriteLine(DateTime.Now);



                            //StreamWriter writer = new StreamWriter("myfile.txt");
                            // FileStream fs = new FileStream("myfile.txt", FileMode.Create);
                            // First, save the standard output.
                            //TextWriter tmp = Console.Out;
                            //StreamWriter sw = new StreamWriter(fs);


                            //writer.Close();    
                        }*/
                        //writer.Close();
                    }



                    /*  if (skeleton != null)
                      {
                          Joint j = skeleton.Joints[JointType.KneeLeft];

                          if (j.TrackingState == JointTrackingState.Tracked)
                          {
                              Console.WriteLine("The output of X Y Z coordinates");

                              Console.WriteLine("Head: X AXIS" + j.Position.X + ",Y AXIS \t " + j.Position.Y + ",Z AXIS\t " + j.Position.Z);
                          }
                      }*/


                }

            }
        }





        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StopKinect(kinectSensorChooser1.Kinect);

        }




        void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.Stop();
            }

        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }





    }
}

       