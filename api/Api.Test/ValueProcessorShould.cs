using Api.Domain;
using Xunit;
using Moq;

namespace Api.Test
{
    public class ValueProcessorShould
    {
        [Fact]
        public void GetValuesFromPermanentStoreByDefault()
        {
            var permanentStore = new Mock<IPermanentStore>();
            var memoryStore = new Mock<IMemoryStore>();
            
            var valuesProcessor = new ValuesProcessor(permanentStore.Object, memoryStore.Object);

            valuesProcessor.Get(1);
            
            permanentStore.Verify(s => s.Get(It.IsAny<int>()), Times.Once());
        }
        
        [Fact]
        public void GetValuesFromMemoryStoreIfAny()
        {
            var permanentStore = new Mock<IPermanentStore>();
            var memoryStore = new Mock<IMemoryStore>();

            memoryStore.Setup(m => m.Get(It.IsAny<int>()))
                       .Returns(new ValueType { Id = 1, Value = "someValue"});

            var valuesProcessor = new ValuesProcessor(permanentStore.Object, memoryStore.Object);

            valuesProcessor.Get(1);
            
            permanentStore.Verify(s => s.Get(It.IsAny<int>()), Times.Never());
            memoryStore.Verify(s => s.Get(It.IsAny<int>()), Times.Once());
        }
        
        [Fact]
        public void SaveValues()
        {
            var permanentStore = new Mock<IPermanentStore>();
            var memoryStore = new Mock<IMemoryStore>();
            
            var valuesProcessor = new ValuesProcessor(permanentStore.Object, memoryStore.Object);

            var valueType = new ValueType(){Id = 1, Value = "newValue"};
            valuesProcessor.Save(valueType);
            
            permanentStore.Verify(s => s.Save(valueType), Times.Once());
            memoryStore.Verify(s => s.Save(valueType), Times.Once());

        }
    }
}