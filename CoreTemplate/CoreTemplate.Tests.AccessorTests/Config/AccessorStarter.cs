using AutoMapper;
using CoreTemplate.Accessors.Config;
using System;

namespace CoreTemplate.Tests.AccessorTests.Config
{
    public class AccessorStarter : IDisposable
    {
        public AccessorStarter()
        {
            //Set up AutoMapper
            Mapper.Initialize(config =>
            {
                config.AddProfile<AccessorMapper>();
            });
        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}