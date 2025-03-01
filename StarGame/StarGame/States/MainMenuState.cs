﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivid3D;
using Vivid3D.App;
using Vivid3D.Draw;
using Vivid3D.FX;
using Vivid3D.FXS;
using Vivid3D.Input;
using Vivid3D.Scene;
using Vivid3D.Tex;
using Vivid3D.Util;
using Vivid3D.VFX;
using Vivid3D.Sound;
using Vivid3D.Reflect;
using System.Reflection;
using Vivid3D.Archive;
using Vivid3D.Lighting;
using Vivid3D.PostProcess;
using Vivid3D.Import;
using Vivid3D.Material;
using Vivid3D.State;
using Vivid3D.Texture;
using Vivid3D.Logic;
using Vivid3D.Resonance;
using Vivid3D.App;
using OpenTK;
using Vivid3D.ParticleSystem;
using Vivid3D.PostProcess.Processes;
using Vivid3D.Resonance.Forms;
using Vivid3D.Physics;
namespace StarKnight.States
{
    public class MainMenuState : VividState
    {


        public UI UI = null;
        public ImageForm BG;
        private VTex2D MenuBG;
        public VSound Music;

        public GraphCam3D cam1 = null;
        public SceneGraph3D scene3d = null;
        public GraphNode3D ent1 = null;
        public GraphLight3D light1 = null;
        public PostProcessRender ppRen;
        public ParticleEmitter pe1;
        public Particle pb1;
        public override void InitState()
        {



            MenuBG = new VTex2D("Data\\2D\\Backgrounds\\MainMenu\\menubg.jpg", LoadMethod.Single, false);

            Music = StarSoundSys.Play2DFile("Data\\Music\\Menu\\MainMenu\\MenuTheme1.wav");

            UI = new UI();



            UI.Root = new ImageForm().Set(0, 0, Vivid3D.App.VividApp.W, Vivid3D.App.VividApp.H,"ImageForm").SetImage(MenuBG);

            WindowForm win1 = (WindowForm)(new WindowForm().Set(40, 100, 260, 400, "Star Knights"));

            UI.Root = win1;
           

            win1.NoResize();


            

           // UI.Root.Add(win1);


            var b1 = new ButtonForm().Set(20, 30, 200, 25, "Less");
            var b2 = new ButtonForm().Set(20,60,200,25,"More");

            win1.Add(b1);
            win1.Add(b2);

            void LessFunc(int b)
            {
                dc = dc - 0.1f;
                Console.WriteLine("DC:" + dc);

            }
            void MoreFunc(int b)
            {
                dc = dc + 0.1f;
                Console.WriteLine("DC:" + dc);
            }

            b1.Click = LessFunc;
            b2.Click = MoreFunc;


            var test = win1.Forms[0];




            test.Click = (b) =>
            {

                Console.WriteLine("Yep!");

            };

            ppRen = new Vivid3D.PostProcess.PostProcessRender(512, 512);
            Console.WriteLine("Creating 3D Scene graph.");
            scene3d = new SceneGraph3D();

            ppRen.Scene = scene3d;
           
            Console.WriteLine("Importing mesh.");
            AssImpImport.ScaleX = 0.02f;
            AssImpImport.ScaleY = 0.02f;
            AssImpImport.ScaleZ = 0.02f;
            ent1 = Import.ImportNode("Data\\3D\\box.3ds");
            AssImpImport.ScaleX = 1;
            AssImpImport.ScaleY = 1;
            AssImpImport.ScaleZ = 1;

            var e2 = Import.ImportNode("Data\\3D\\floor.3ds");
            var e3 = e2 as GraphEntity3D;
           // e3.ScaleMeshes(10, 1, 10);
            Console.WriteLine("Set up.");
            var mat1 = new Material3D();
            //Console.WriteLine("Loading texture.");
            mat1.TCol = new VTex2D("Data\\3D\\brick_2.png",LoadMethod.Single,false);
            mat1.TNorm = new VTex2D("Data\\3D\\brick_2_NRM.png", LoadMethod.Single,false);
            Console.WriteLine("Loaded.");


            var ge = ent1 as GraphEntity3D;
            var g2 = e2 as GraphEntity3D;
            ge.SetMat(mat1);
            g2.SetMat(mat1);

            cam1 = new GraphCam3D();
            cam1.LocalPos = new OpenTK.Vector3(0, 50,60);


            cam1.LookAt(ent1);

            ent1.LocalPos = new Vector3(0, 30, 0);

            var bb = ent1 as GraphEntity3D;
            var br = bb.Bounds;
            
            Console.WriteLine("Bounds:" + br.W +" " + br.H + " " + br.D);
         



            light1 = new Vivid3D.Lighting.GraphLight3D();
            var l2 = new Vivid3D.Lighting.GraphLight3D();
            var l3 = new Vivid3D.Lighting.GraphLight3D();

            l3.LocalPos = new OpenTK.Vector3(30, 80, 45);
            l3.Diff = new OpenTK.Vector3(0, 1, 2);

            l2.LocalPos = new OpenTK.Vector3(5, 20, 50);
            l2.Diff = new OpenTK.Vector3(2, 2, 1);


            light1.LocalPos = new OpenTK.Vector3(0, 80, 350);

          //  ent1.Rot(new OpenTK.Vector3(0, 45, 0), Space.Local);



            scene3d.Add(ent1);
            scene3d.Add(e2);
           scene3d.Add(l2);


            scene3d.Add(l3);

            scene3d.Add(light1);

            scene3d.Add(cam1);

            light1.Diff = new OpenTK.Vector3(3, 3, 3);


            var mbpp = new PPMotionBlur();
            ppRen.Add(mbpp);

            //ppRen.Add(new VPPBlur());
            //VPPBlur  bp = (VPPBlur)ppRen.Processes[0];

            //bp.Blur = 0.9f;

            pe1 = new ParticleEmitter();
            pe1.Graph = scene3d;
            pb1 = new Particle(32, 32);
            pb2 = new Particle(32, 32);
            pb1.Tex = new VTex2D("Data\\particle\\part1.png",LoadMethod.Single,true);
            pb2.Tex = new VTex2D("Data\\particle\\part2.png", LoadMethod.Single, true);

            Console.WriteLine("Setting up physicsSDK.");
            PhysicsManager.InitSDK();
            var ge1 = ent1 as GraphEntity3D;
            var gr2 = e2 as GraphEntity3D;

            ge1.EnablePy(PyType.Box);
            gr2.EnablePy(PyType.Mesh);
            scene3d.BeginFrame();
            Console.WriteLine("Setup ok.");


        }

        Particle pb2;
        public override void UpdateState()
        {
            //  Console.WriteLine("Update");
            PhysicsManager.Update(0.08f);
            UI.Update();
            //pe1.Update();
            scene3d.Update();

        }
        public bool firstd = true;
        public int lx, ly;
        public override void DrawState()
        {
            //Console.WriteLine("Draw");

            int move = 8;
            if (VInput.KeyIn(OpenTK.Input.Key.ShiftLeft))
            {
                move = 12;
            }
            if (VInput.KeyIn(OpenTK.Input.Key.W))
            {

                cam1.Move(new OpenTK.Vector3(0, 0, -move), Space.Local);

            }
            if (VInput.KeyIn(OpenTK.Input.Key.A))
            {
                cam1.Move(new OpenTK.Vector3(-move, 0, 0), Space.Local);
            }

            if (VInput.KeyIn(OpenTK.Input.Key.D))
            {
                cam1.Move(new OpenTK.Vector3(move, 0, 0), Space.Local);
            }

            if (VInput.KeyIn(OpenTK.Input.Key.S))
            {
                cam1.Move(new OpenTK.Vector3(0, 0, move), Space.Local);
            }
            if (VInput.MB[0])
            {
                light1.LocalPos = cam1.LocalPos;
            }
            if (VInput.MB[1])
            {
                /*
                Random rnd = new Random(Environment.TickCount);

                Vector3 ri = new Vector3(rnd.Next(-4, 4), rnd.Next(-4, 4), rnd.Next(-4, 4));

                //Console.WriteLine("yeah");
                pe1.Emit(pb1, new OpenTK.Vector3(0, 0, 0), ri);

                ri = new Vector3(rnd.Next(-4, 4), rnd.Next(-4, 4), rnd.Next(-4, 4));

                pe1.Emit(pb2, new Vector3(0, 10, 0), ri);
                */

            }
            int xd, yd;
            if (firstd)
            {
                lx = VInput.MX;
                ly = VInput.MY;
                firstd = false;
            }
            xd = VInput.MX - lx;
            yd = VInput.MY - ly;
            lx = VInput.MX;
            ly = VInput.MY;
           
                cam1.Turn(new OpenTK.Vector3(-yd, -xd, 0), Space.Local);
           
            //ppRen.Render();
            scene3d.RenderShadows();
            ppRen.Render();

            if (fx1 == null)
            {
     //           fx1 = new EVelBuf();
            }
    
   //         Vivid3D.Effect.FXG.FXOverride = fx1;
            //scene3d.RenderNoLights();
           // Vivid3D.Effect.FXG.FXOverride = null;

            //ppRen.Render();

            //scene3d.Render();

            scene3d.BeginFrame();

            //pe1.Render();
            //scene3d.Render();
            //ppRen.Render();






           UI.Render();


        }
        float dc = 0;
        EVelBuf fx1 = null;

    }
}
