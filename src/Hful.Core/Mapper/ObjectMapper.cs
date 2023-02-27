using AutoMapper;

namespace Hful.Core.Mapper
{
    internal class ObjectMapper : IObjectMapper
    {
        private readonly IMapper _mapper;

        public ObjectMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource obj)
        {
            return _mapper.Map<TSource, TDestination>(obj);
        }

        public List<TDestination> MapList<TSource, TDestination>(List<TSource> obj)
        {
            return _mapper.Map<List<TSource>, List<TDestination>>(obj);
        }

        public TDestination Map<TSource, TDestination>(TSource obj, TDestination d)
        {
            return _mapper.Map(obj, d);
        }
    }
}
