using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;
using GLEED2D.Forms;
using Gleed2D.Core;
using Gleed2D.Core.CustomUITypeEditors;
using Gleed2D.InGame;
using Microsoft.Xna.Framework;
using StructureMap;

namespace GLEED2D
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            ObjectFactory.Configure(
                a =>
                {
                    a.For<IEventHub>().Singleton().Use<EventHub>();
                    a.For<IModelEventHub>().Singleton().Use<ModelEventHub>();
                    a.For<IDisk>().Singleton().Use<Disk>();
                    a.For<IMenuItems>().Singleton().Use<MainFormMenuItems>();
                    a.For<IGleedRenderer>().Singleton().Use<GleedRenderer>();
                    a.For<IMemento>().Singleton().Use<Mememto>();
                    a.For<IGetAssemblyInformation>().Singleton().Use<GetAssemblyInformation>();
                    a.For<IModel>().Singleton().Use<Model>();
                    a.For<IHandleUserActions>().Singleton().Use<HandleUserActions>();
                    a.For<IHandleKeyboardCommands>().Singleton().Use<HandleKeyboardCommands>();
                    a.For<IImageRepository>().Singleton().Use<ImageRepository>().Named(@"iconImages");
                    a.For<IImageRepository>().Singleton().Use<ImageRepository>();
                    a.For<ILoadImages>().Singleton().Use<LoadImages>();
                    a.For<IExtensibility>().Singleton().Use<Extensibility>();
                    a.For<IGame>().Singleton().Use<XnaGame>();
                    a.For<IMainForm>().Singleton().Use<MainForm>();
                    a.For<ICanvas>().Singleton().Use<Canvas>();
                    a.For<ITextureStore>().Singleton().Use<TextureStore>();
                    a.For<IDrawing>().Singleton().Use<Drawing>();
                });

            TypeDescriptor.AddAttributes(
                typeof (PathToFolder), 
                new EditorAttribute(typeof (PathToFolderUiTypeEditor), typeof (UITypeEditor)));

            TypeDescriptionProvider descriptionProvider = TypeDescriptor.AddAttributes(typeof (PathToFolder), new TypeConverterAttribute(typeof (PathToFolderConverter)));
            AttributeCollection attributeCollection = TypeDescriptor.GetAttributes(typeof (PathToFolder));
//            descriptionProvider.

            TypeDescriptionProvider typeDescriptionProvider = TypeDescriptor.AddAttributes(typeof (Color), new EditorAttribute(typeof (XnaColorUiTypeEditor), typeof (UITypeEditor)));

            TypeDescriptor.AddAttributes(
                typeof (PathToFile),
                new EditorAttribute(typeof (PathToFileUiTypeEditor), typeof (UITypeEditor)));

            TypeDescriptor.AddAttributes(
                typeof (LinkedItem),
                new EditorAttribute(typeof (LinkedItemTypeConverter), typeof (UITypeEditor)));

            TypeDescriptor.AddAttributes(
                typeof (PathToFile),
                new TypeConverterAttribute(typeof (PathToFileConverter)));

            TypeDescriptor.AddAttributes(
                typeof (PathToFolder),
                new TypeConverterAttribute(typeof (PathToFolderConverter)));

            if (Debugger.IsAttached)
            {
                runInDebugger();
            }
            else
            {
                runNormally();
            }
        }

        private static void runNormally()
        {
            try
            {
                var form = ObjectFactory.GetInstance<IMainForm>();

                form.Show();

                var game = ObjectFactory.GetInstance<IGame>();

                game.Run();
            }
            catch (Exception e)
            {
                if (Debugger.IsAttached)
                {
                    throw;
                }

                reportError(e);
            }
            finally
            {
                Logger.Instance.log("Application ended.");
            }
        }

        static void runInDebugger()
        {
            var form = ObjectFactory.GetInstance<IMainForm>();

            form.Show();

            var game = ObjectFactory.GetInstance<IGame>();

            game.Run();
        }

        static void reportError(Exception e)
        {
            reportError(e, false);
        }

        static void reportError(Exception e, bool @continue)
        {
            Logger.Instance.log(@"Exception caught: 

 {0}

{1}".FormatWith(e.Message, e.StackTrace));

            if (e.InnerException != null)
            {
                Logger.Instance.log("Inner Exception: {0}".FormatWith(e.InnerException.Message));
            }

            if (Debugger.IsAttached)
                throw e;

            MessageBox.Show(@"An exception was caught. Application will end. Please check the file log.txt.");

            if (!@continue)
            {
                throw e;
            }
        }
    }
}