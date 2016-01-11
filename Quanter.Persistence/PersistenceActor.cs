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

        public PersistenceActor()
        {
            _log.Debug("");
        }

        protected override void OnReceive(object message)
        {
            PersistenceRequest pr = message as PersistenceRequest;
            if(pr != null)
            {
                switch(pr.Type)
                {
                    case PersistenceType.INIT_DATABASE:
                        _createTables();
                        break;
                    case PersistenceType.OPEN:
                        _log.Debug("初始化，并打开Session");
                        _init();
                        _openSession();
                        break;
                    case PersistenceType.SAVE:
                        _save(pr.Body);
                        break;
                    case PersistenceType.LOAD:
                        //_load((int)pr.Body);
                        break;
                    case PersistenceType.UPDATE:
                        _update(pr.Body);
                        break;
                    case PersistenceType.LIST:
                        _list((String)pr.Body);
                        break;
                    case PersistenceType.FIND:
                        _find((String)pr.Body);
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
                //export.SetOutputFile("./db/create_tables.sql");
                export.Drop(true, true);
                export.Create(false, true);
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
            try {
                session.Save(obj);
                Sender.Tell(obj);

            } catch (Exception e)
            {
                _log.Error("保存发生异常 {0}", e.StackTrace);
            }
        }

        private void _update(Object obj)
        {
            _log.Debug("更新对象 {0}", obj.GetType().ToString());
            session.Update(obj);
        }

        private void _delete(Type theType, int id)
        {
            Object obj = session.Load(theType, id); 
            _delete(obj);
        }

        private void _delete(Object obj)
        {
            session.Delete(obj);
        }

        private void _load(Type theType, int id)
        {
            object ret = null;
            ret = session.Load(theType, id);
            Sender.Tell(ret);
        }

        private void _find(String where)
        {
            _log.Info("{1}Find 数据{0}", where, this.ToString());
            try
            {
                object ret = null;
                IList objs = session.CreateQuery(where).List();
                if (objs != null && objs.Count != 0) ret = objs[0];

                Sender.Tell(ret);
            }catch (Exception e)
            {
                _log.Error("{1}发送异常{0}", e.StackTrace, this.ToString());
            }
        }

        private void _list(string where)
        {
            IList ret = null;
            try
            {
                IQuery q = session.CreateQuery(where);
                ret = q.List();
            }
            catch (Exception e)
            {
                _log.Error("发生异常 {0}", e.StackTrace);
            } finally
            {
                Sender.Tell(ret);
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
            _log.Info("关闭数据库链接");
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
