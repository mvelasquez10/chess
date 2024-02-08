using Xunit;

namespace Chess.Domain.Test
{
    public class EntityTest
    {
        private record TestEntity : Entity
        {
        }

        #region Public Methods

        [Fact]
        public void Entity_GetId()
        {
            var id = (short)1;
            var testEntity = new TestEntity { Id = id };

            Assert.Equal(id, testEntity.Id);
        }

        #endregion Public Methods
    }
}