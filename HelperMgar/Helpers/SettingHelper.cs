using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
namespace NaiveInkCanvas.Helpers
{
    public class SettingHelper
    {
        public static SettingHelper Default => new SettingHelper();
        public static ApplicationDataContainer LocContainer => ApplicationData.Current.LocalSettings;
        public static ApplicationDataContainer ProjectContainer =>
            ApplicationData.Current.LocalSettings.CreateContainer("Projects", ApplicationDataCreateDisposition.Always);
        public static ApplicationDataContainer BackgroundContainer =>
            ApplicationData.Current.LocalSettings.CreateContainer("pjbackground", ApplicationDataCreateDisposition.Always);

        private SettingHelper() { }
        /// <summary>
        /// 搜索Setting容器
        /// </summary>
        /// <param name="con">根容器</param>
        /// <param name="name">容器名</param>
        /// <returns>如不存在，返回null</returns>
        public ApplicationDataContainer GetContainer(ApplicationDataContainer con, string name)
        {
            foreach (var c in con.Containers)
            {
                if (c.Key == name)
                    return c.Value;
            }
            return null;
        }
        /// <summary>
        /// 获取设置的值
        /// </summary>
        /// <param name="con">目标容器</param>
        /// <param name="key">键</param>
        /// <returns>如果没有数据，value=null</returns>
        public KeyValuePair<string, object> GetSettingValue(ApplicationDataContainer con, string key)
        {
            return new KeyValuePair<string, object>(key, con.Values.Keys.Contains(key) ? con.Values[key] : null);
        }
        /// <summary>
        /// 设置设置的值，如不存在，自动添加
        /// </summary>
        /// <param name="con">目标容器</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="sameexit">相同是否退出</param>
        public bool SetSettingValue(ApplicationDataContainer con, string key, string value, bool sameexit = false)
        {
            var b = false;
            if (con.Values.Keys.Contains(key))
            {
                con.Values[key] = value;
                b = true;
            }
            else if (!sameexit)
            {
                con.Values.Add(key, value);
            }
            return b;
        }
        /// <summary>
        /// 不理键，将集合到字符串存入容器
        /// </summary>
        /// <typeparam name="TVALUES">集合数据类型</typeparam>
        /// <param name="con">目标容器</param>
        /// <param name="values">值</param>
        public void SetSettingValues<TVALUES>(ApplicationDataContainer con, IList<TVALUES> values)
            where TVALUES : class
        {
            for (int i = 0, j = 0; i < values.Count(); i++, j++)
            {
                //TODO:将数据tostring加入容器
                if (!SetSettingValue(con, j.ToString(), values[i].ToString()))
                    i--;
            }
        }
        /// <summary>
        /// 设置设置用新的容器
        /// </summary>
        /// <param name="con">根容器</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="newname">容器名</param>
        /// <returns>创建后的容器</returns>
        public ApplicationDataContainer SetSettingValueByNewContainer(ApplicationDataContainer con, string key, string value, string newname)
        {
            var ncon = con.CreateContainer(newname, ApplicationDataCreateDisposition.Always);
            SetSettingValue(ncon, key, value);
            return ncon;
        }
        /// <summary>
        /// 新建一个容器不管键，把全部值tostring放入
        /// </summary>
        /// <typeparam name="TVALUE">值类型</typeparam>
        /// <param name="con">目标容器</param>
        /// <param name="values">值列表</param>
        /// <param name="newname">容器名字</param>
        /// <returns>返回新建立的容器</returns>
        public ApplicationDataContainer SetSettingValuesByNewContainer<TVALUE>(ApplicationDataContainer con, IList<TVALUE> values, string newname)
            where TVALUE : class
        {
            var ncon = con.CreateContainer(newname, ApplicationDataCreateDisposition.Always);
            SetSettingValues(ncon, values);
            return ncon;
        }
        /// <summary>
        /// 取容器所有数据
        /// </summary>
        /// <param name="con">目标容器</param>
        /// <returns>数据列表</returns>
        public IList<KeyValuePair<string, object>> GetValues(ApplicationDataContainer con)
        {
            return con.Values.ToList();
        }
        /// <summary>
        /// 删除容器
        /// </summary>
        /// <param name="con">根容器</param>
        /// <param name="name">想删除容器的名字</param>
        public void DeleteContainer(ApplicationDataContainer con, string name)
        {
            try
            {
                con.DeleteContainer(name);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }
    }
}
