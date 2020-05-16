using System.Collections.Generic;

namespace Trs.IntegerMapper.Memoization
{
    public class Memoizer<TInput, TOutput>
    {
        private readonly IIntegerMapper<TInput> _inputMapper;
        private readonly IIntegerMapper<TOutput> _outputMapper;

        private readonly Dictionary<ulong, ulong> _existingMappings;

        public Memoizer(IIntegerMapper<TInput> inputMapper,
                        IIntegerMapper<TOutput> outputMapper)
        => (_inputMapper, _outputMapper, _existingMappings) = (inputMapper, outputMapper, new Dictionary<ulong, ulong>());

        /// <summary>
        /// Caches related input and output. If input is already present,
        /// existing values are overwritten.
        /// </summary>
        public void Memoize(TInput input, TOutput output)
        => _existingMappings[_inputMapper.Map(input)] = _outputMapper.Map(output);

        /// <summary>
        /// Gets the previously saved output for a given input.
        /// If there is no 
        /// </summary>
        /// <param name="input">The input value to look up</param>
        /// <returns>The previously saved output values, or null/default for <see cref="TOutput"/></returns>
        public TOutput GetOutput(TInput input)
        => _existingMappings.TryGetValue(_inputMapper.Map(input), out ulong mappedOutput) switch
        {
            true => _outputMapper.ReverseMap(mappedOutput),
            _ => default
        };
    }
}
