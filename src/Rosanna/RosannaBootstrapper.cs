using System;
using System.Linq;
using Nancy;
using TinyIoC;

namespace Rosanna
{
    public class RosannaBootstrapper : DefaultNancyBootstrapper
    {
        public TinyIoCContainer Container
        {
            get { return container; }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(typeof(IRosannaConfiguration), FindConfigurationType()).AsSingleton();
        }

        private static Type FindConfigurationType()
        {
            Type customConfiguration = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                        from type in assembly.GetTypes()
                                        where !type.IsAbstract
                                        where type != typeof(RosannaConfiguration)
                                        where typeof(IRosannaConfiguration).IsAssignableFrom(type)
                                        select type).FirstOrDefault();

            return customConfiguration ?? typeof(RosannaConfiguration);
        }
    }
}