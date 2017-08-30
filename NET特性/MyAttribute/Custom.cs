using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;    //引用BindingFlags需要

namespace TestAttribute
{
    /// <summary>
    /// 使用自定义属性，并获取方法的属性
    /// </summary>
    public class Custom
    {
        public Custom()
        {
            var syncAttribute = Attribute.GetCustomAttribute(this.GetType().GetMethod(nameof(Working), BindingFlags.NonPublic | BindingFlags.Instance), typeof(SyncWorkAttribute)) as SyncWorkAttribute;
            if (syncAttribute != null)
            {
                _enableSyncWork = true;
            }
            else
            {
                var asyncAttribute = Attribute.GetCustomAttribute(this.GetType().GetMethod(nameof(Working), BindingFlags.NonPublic | BindingFlags.Instance), typeof(AsyncWorkAttribute)) as AsyncWorkAttribute;
                if (asyncAttribute != null)
                {
                    _enableSyncWork = false;
                }
            }
        }

        public bool _enableSyncWork { get; }

        [SyncWork]
        protected void Working()
        {

        }


        public static void TEST_CUSTOM()
        {
            Custom custom = new Custom();
            bool bSync = custom._enableSyncWork;
        }
    }

    /// 前面使用了SyncWorkAttribute和AsyncWorkAttribute两个属性，需要在这定义

    /// <summary>
    /// 使用同步的方式执行
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public class SyncWorkAttribute : Attribute
    {
    }

    /// <summary>
    /// 使用异步的方式执行
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncWorkAttribute : Attribute
    {
    }

}
