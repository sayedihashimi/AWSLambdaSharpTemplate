﻿using System;
using System.Threading.Tasks;
using Amazon.Lambda.TestUtilities;
using Kralizek.Lambda;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Tests.Lambda
{
    public class RequestResponseFunctionTests
    {
        private RequestResponseFunction CreateSystemUnderTest()
        {
            return new RequestResponseFunction();
        }

        [Fact]
        public void Configure_should_be_invoked_on_type_initialization()
        {
            var sut = CreateSystemUnderTest();

            Assert.True(sut.IsConfigureInvoked);
        }

        [Fact]
        public void ConfigureServices_should_be_invoked_on_type_initialization()
        {
            var sut = CreateSystemUnderTest();

            Assert.True(sut.IsConfigureServicesInvoked);
        }

        [Fact]
        public void ConfigureLogging_should_be_invoked_on_type_initialization()
        {
            var sut = CreateSystemUnderTest();

            Assert.True(sut.IsConfigureLoggingInvoked);
        }

        [Fact]
        public async Task FunctionHandlerAsync_throws_if_no_handler_is_registered()
        {
            var sut = CreateSystemUnderTest();

            var context = new TestLambdaContext();

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.FunctionHandlerAsync("Hello World", context));
        }

        public class RequestResponseFunction : RequestResponseFunction<string, string>
        {
            protected override void Configure(IConfigurationBuilder builder) => IsConfigureInvoked = true;

            protected override void ConfigureServices(IServiceCollection services) => IsConfigureServicesInvoked = true;

            protected override void ConfigureLogging(ILoggerFactory loggerFactory, IExecutionEnvironment executionEnvironment) => IsConfigureLoggingInvoked = true;

            public bool IsConfigureInvoked { get; private set; }

            public bool IsConfigureServicesInvoked { get; private set; }

            public bool IsConfigureLoggingInvoked { get; private set; }
        }
    }

}
