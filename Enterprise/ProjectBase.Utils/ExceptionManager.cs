using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Enterprise.Model;

namespace ProjectBase.ErrorHandle
{
    public static class ExceptionManager
    {
        private static readonly List<Type> exceptionPolicies = new List<Type> 
        {
            typeof(OutOfMemoryException),
            typeof(StackOverflowException)         
        };

        public static string FullMessage(this Exception exceptionToHandle)
        {
            var builder = new StringBuilder();
            while (exceptionToHandle != null)
            {
                builder.AppendFormat("{0}{1}", exceptionToHandle, Environment.NewLine);
                exceptionToHandle = exceptionToHandle.InnerException;
            }
            return builder.ToString();
        }

        public static void Process(Action tryAction, 
            Func<Exception, bool> isRecoverPossible, 
            Action<Exception> handlerAction)
        {
            try 
            {
                tryAction();
            }
            catch (Exception ex)
            {
                if (!isRecoverPossible(ex))
                {
                    throw;
                }
                handlerAction(ex);
                ShowMessage(ex);
                Application.Exit();
            }
        }

        public static TResult Process<TResult>(Func<TResult> func, 
            Func<Exception, bool> isRecoverPossible, 
            Action<Exception> handlerAction)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception ex)
            {
                if (!isRecoverPossible(ex))
                {
                    throw;
                }
                handlerAction(ex);
                ShowMessage(ex);
                Application.Exit();
                return default(TResult);
            }
        }

        public async static Task<TResult> Process<TResult>(Func<Task<TResult>> func,
            Func<Exception, bool> isRecoverPossible,
            Action<Exception> handlerAction)
        {
            try
            {
                return await func.Invoke();
            }
            catch (Exception ex)
            {
                if (!isRecoverPossible(ex))
                {
                    throw;
                }

                handlerAction(ex);
                ShowMessage(ex);
                Application.Exit();
                return default(TResult);
            }
        }       

        public static bool IsFatal(this Exception exceptionToHandle)
        {
            bool status = !NotFatal(exceptionToHandle);
            return status;
        }

        private static bool NotFatal(this Exception exceptionToHandle)
        {
            return exceptionPolicies.All(curFatal => exceptionToHandle.GetType() != curFatal);
        }

        public static void ShowMessage(Exception ex)
        {
            ShowMessage("Error ", ex.Message);
        }

        private static void ShowMessage(string title, string message)
        {
            const string accentedText = "We` re sorry about this ";
            MessageBox.Show(title + accentedText + message);
        }
    }
}
