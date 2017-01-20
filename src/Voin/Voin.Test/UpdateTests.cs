using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voin.Core;
using Voin.Core.Rules.CoreDsl;
using Voin.Core.Rules.EnglishDsl;
using Voin.Test.Models.Actors;
using Voin.Test.Models.Resources;
using Voin.Test.Models.Rights;

namespace Voin.Test
{
    [TestClass]
    public class UpdateTests
    {
        private readonly User alice;
        private readonly User bob;
        private readonly Right see;
        private readonly Right use;
        private readonly Printer redPrinter;
        private readonly Printer pinkPrinter;
        private readonly File imageFile;
        private readonly File videoFile;

        private readonly IRepository<IActor> actors;
        private readonly InMemoryRepository<IResource> resources;
        private readonly InMemoryRepository<IRight> rights;

        public UpdateTests()
        {
            this.alice = new User("Alice");
            this.bob = new User("Bob");
            this.see = new Right("see");
            this.use = new Right("use");
            this.redPrinter = new Printer("Red Printer");
            this.pinkPrinter = new Printer("Pink Printer");
            this.imageFile = new File("Image File");
            this.videoFile = new File("Video File");
            this.actors = new InMemoryRepository<IActor>(new[] { alice, bob });
            this.resources = new InMemoryRepository<IResource>(new IResource[] { redPrinter, pinkPrinter, imageFile, videoFile });
            this.rights = new InMemoryRepository<IRight>(new[] { see, use });
        }

        [TestMethod]
        public void TestAnyUserCanSeeTheRedPrinter()
        {
            var rightService = this.BuildRightService(_ => _.Any<User>().Can(see).The(redPrinter));

            var hasAccess = rightService.HasRight(alice, redPrinter, see);

            Assert.IsTrue(hasAccess);
        }

        private RightService BuildRightService(params Func<Root, ICompleteRule>[] rules)
        {
            var rightService = new RightService(new InMemoryRightStore(), this.actors, resources, rights);

            foreach (var rule in rules)
            {
                rightService.AddRule(rule);
            }

            rightService.Initialize();

            return rightService;
        }
    }
}