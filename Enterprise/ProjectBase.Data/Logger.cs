using System;
using System.Runtime.CompilerServices;
using log4net;

namespace ProjectBase.Data
{
    public class Logger
    {
        private static Logger logger;
        private ILog log;
        public static Logger Instance
        {
            get 
            {
                if (logger == null)
                {
                    logger = new Logger();
                }
                return logger;
            }
        }

        private Logger()
        { }

        private void Initialize()
        {
            log = LogManager.GetLogger(typeof(Logger));
            log4net.Config.XmlConfigurator.Configure();
        }

        public void LogInfo(Entity<long> entity)
        {
            if (log == null)
            {
                Initialize();
            }

            log.Info(string.Format("BookToAuthorId {0}", entity.ID));
        }
        
        public void Error(Exception ex,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (log == null)
            {
                Initialize();
            }
            log.Error(string.Format(
                "Error Logged From method {0} in file {1} at line {2}",
                memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Warning(Exception ex,
           [CallerMemberName] string memberName = "",
           [CallerFilePath] string sourceFilePath = "",
           [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (log == null)
            {
                Initialize();
            }
            log.Warn(string.Format("Warning from method {0} in file {1} at line {2}",
                memberName, sourceFilePath, sourceLineNumber), ex);
        }

        public void Debug(Exception ex,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (log == null)
            {
                Initialize();
            }
            log.Debug(string.Format(
                "Debug entry from method {0} in file {1} at line {2}",
                memberName, sourceFilePath, sourceLineNumber), ex);
        }
    }
}
