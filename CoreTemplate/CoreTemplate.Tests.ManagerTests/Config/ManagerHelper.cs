using AutoMapper;
using CoreTemplate.Accessors.Config;
using System;

namespace CoreTemplate.Tests.ManagerTests.Config
{
    public class ManagerHelper : IDisposable
    {
        public ManagerHelper()
        {
            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<AccessorMapper>();
            });
        }

        public void Dispose()
        {
        }
    }
}