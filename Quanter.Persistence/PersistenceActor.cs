using Akka.Actor;
using Akka.Event;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Quanter.Trader.Messages;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quanter.Persistence
{
    /// <summary>
    /// 处理的消息有 
    /// </summary>
    public class PersistenceActor : UntypedActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        private ISessionFactory factory = null;
        private Configuration configuration = null;
        private ISession session = null;


        protected override void OnReceive(object message)
        {
            PersistenceRequest pr = message as PersistenceRequest;
            if(pr != null)
            {
                switch(pr.Type)
                {
                    case PersistenceType.OPEN:
                        _log.Debug("初始化，并打开Session");
                        // _init();
                        // _openSession();
                        break;
                    case PersistenceType.SAVE:
                        _save(pr.Body);
                        break;
                    case PersistenceType.LOAD:
                        break;
                    case PersistenceType.CLOSE:
                        _log.Debug("关闭Session");
                        // _closeSession();
                        break;
                    default:
                        break;
                }
            }
        }

        private void _createTables() {
            try {
                Configuration _cfg = new Configuration().Configure();
                SchemaExport export = new SchemaExport(_cfg);
                export.SetOutputFile("./db/create_tables.sql");
                export.Drop(true, true);
                export.Create(true, true);
            } catch (Exception e)
            {
                _log.Error("创建数据库语句发生异常。 {0}", e.StackTrace);
            }
        }

        private void _init()
        {
            _log.Info("初始化Persistence");
            try
            {
                configuration = new Configuration();
                configuration.Configure();
                configuration.AddAssembly(Assembly.GetExecutingAssembly());
                factory = configuration.BuildSessionFactory();
            }
            catch (Exception e)
            {
                _log.Error("初始化HIBERNATE，发生异常：{0}", e.StackTrace);
            }
        }

        private void _save(Object obj)
        {
            _log.Debug("保存对象 {0}", obj.GetType().ToString());
            session.Save(obj);
        }

        private void _update(Object obj)
        {
            _log.Debug("更新对象 {0}", obj.GetType().ToString());
            // session.Update(obj);
        }

        private void _delete(Type theType, int id)
        {
            Object obj = _load(theType, id);
            _delete(obj);
        }

        private void _delete(Object obj)
        {
            session.Delete(obj);
        }

        private object _load(Type theType, int id)
        {
            return session.Load(theType, id);
        }

        private Object _find(String where)
        {
            IList objs = session.CreateQuery(where).List();
            if (objs == null || objs.Count == 0) return null;

            return objs[0];
        }

        private IList _list(string where)
        {
            try
            {
                IQuery q = session.CreateQuery(where);
                return q.List();
            }
            catch (Exception e)
            {
                // TODO: LOG
                _log.Debug("发生异常 {0}", e.StackTrace);
                return null;
            }
        }

        private ISession _openSession()
        {
            if (session == null)
                session = factory.OpenSession();

            session.FlushMode = FlushMode.Always;
            return session;
        }

        private void _closeSession()
        {
            if (session != null)
            {
                session.Close();
                session = null;
            }
        }

        protected override void PreStart()
        {
            base.PreStart();
        }

        protected override void PostStop()
        {
            base.PostStop();
        }

    }
}
