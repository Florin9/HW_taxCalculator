using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;
using TaxCalculator.Data.Entities;
using TaxCalculator.Data.Implementation.Repositories;
using TaxCalculator.Data.Interfaces.Repositories;
using TaxCalculator.Domain.Common;
using Xunit;

namespace TaxCalculator.Data.Test.Implementation.Repositories
{
    public class TaxBandCacheRepositoryShould
    {
        private Mock<IMemoryCache> _memoryCacheMock;
        private Mock<ITaxBandRepository> _taxBandRepositoryMock;
        private IConfiguration _configuration;
        private ITaxBandCacheRepository _taxBandCacheRepository;
        private static IEnumerable<TaxBand> _cachedTaxBands = new List<TaxBand> { new TaxBand { Id = Guid.NewGuid() } };
        private static IEnumerable<TaxBand> _repositoryTaxBands = new List<TaxBand> { new TaxBand { Id = Guid.NewGuid() } };

        public TaxBandCacheRepositoryShould()
        {
            _memoryCacheMock = new Mock<IMemoryCache>();
            _taxBandRepositoryMock = new Mock<ITaxBandRepository>();

            var inMemory = new Dictionary<string, string>
            {
                { GlobalVariables.CacheKeyTimeoutSeconds, "20" },
            };
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemory)
                .Build();

            _taxBandCacheRepository =
                new TaxBandCacheRepository(_taxBandRepositoryMock.Object, _memoryCacheMock.Object, _configuration);

            SetupMemoryCacheMock(_cachedTaxBands, true);
            SetupRepositoryMock(_repositoryTaxBands);
            SetupSetMemoryCacheMock(_repositoryTaxBands);
        }

        private void SetupMemoryCacheMock(object expectedValue, bool result)
        {
            _memoryCacheMock
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue))
                .Returns(result);
        }

        private void SetupSetMemoryCacheMock(object expectedValue)
        {
            _memoryCacheMock
                .Setup(x => x.CreateEntry(GlobalVariables.TaxBandsCacheKey))
                .Returns(Mock.Of<ICacheEntry>);
        }

        private void SetupRepositoryMock(IEnumerable<TaxBand> result)
        {
            _taxBandRepositoryMock
                .Setup(x => x.GetAllTaxBands())
                .ReturnsAsync(result);
        }

        [Fact]
        public async Task ReturnValueFromCacheWhenCacheContainsKey()
        {
            // arrange

            // act
            var result = await _taxBandCacheRepository.GetAllTaxBands();

            // assert
            result.Should().BeEquivalentTo(_cachedTaxBands);
        }

        [Fact]
        public async Task ReturnValueFromRepositoryWhenCacheNotContainKey()
        {
            //arrange
            SetupMemoryCacheMock(_cachedTaxBands, false);

            // act
            var result = await _taxBandCacheRepository.GetAllTaxBands();

            // assert
            result.Should().BeEquivalentTo(_repositoryTaxBands);
        }

        [Fact]
        public async Task AddValueFromRepositoryToCacheWhenCacheNotContainsKey()
        {
            //arrange
            SetupMemoryCacheMock(_cachedTaxBands, false);

            // act
            var result = await _taxBandCacheRepository.GetAllTaxBands();

            // assert
            _memoryCacheMock.Verify(x => x.CreateEntry(It.IsAny<object>()), Times.Once());
            result.Should().BeEquivalentTo(_repositoryTaxBands);
        }
    }
}