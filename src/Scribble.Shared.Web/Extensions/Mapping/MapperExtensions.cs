using System.Reflection;
using AutoMapper;
using Scribble.Shared.Models;
using Scribble.Shared.Web.Models;

namespace Scribble.Shared.Web.Extensions.Mapping;

public static class MapperExtensions
{
    public static TDestination AsViewModel<TDestination>(this IMapper mapper, object source)
        => mapper.Map<TDestination>(source);
    
    public static IViewModel AsViewModel<TInnerModel>(this IMapper mapper, TInnerModel innerModel)
    {
        var innerModelType = typeof(TInnerModel);

        var viewModelType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(type => type.IsAssignableFrom(typeof(IViewModel)) && type.Name == $"{innerModelType.Name}ViewModel");

        if (viewModelType == null)
            throw new SuitableViewModelNotFoundException(
                $"Couldn't find a suitable view model for this inner model '{innerModelType.Name}'");

        return (IViewModel)mapper.Map(innerModel, innerModelType, viewModelType);
    }

    public static TDestination AsInnerModel<TDestination>(this IMapper mapper, object source) =>
        mapper.Map<TDestination>(source);
    
    public static IEntity AsInnerModel<TViewModel>(this IMapper mapper, TViewModel viewModel)
    {
        var viewModelType = typeof(TViewModel);

        var viewModelIndex = viewModelType.Name.IndexOf("ViewModel", StringComparison.Ordinal);

        var innerModelType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(type =>
                type.IsAssignableFrom(typeof(IEntity)) && type.Name == viewModelType.Name.Remove(viewModelIndex));

        if (innerModelType == null)
            throw new SuitableInnerModelNotFoundException(
                $"Couldn't find a suitable inner model for this view model '{viewModelType.Name}'");

        return (IEntity)mapper.Map(viewModel, viewModelType, innerModelType);
    }
}

public class SuitableViewModelNotFoundException : Exception
{
    public SuitableViewModelNotFoundException(string message) 
        : base(message) { }
}

public class SuitableInnerModelNotFoundException : Exception
{
    public SuitableInnerModelNotFoundException(string message)
        : base(message) { }
}