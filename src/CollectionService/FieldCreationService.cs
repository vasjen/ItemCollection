
using Common.EFCore;
using Common.Core.Entities;
using Common.Repositories;

namespace CollectionService
{
    public  class FieldCreationService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly AppDbContext _context;
        private readonly IFieldRepository<Field> _repository;

        public FieldCreationService(AppDbContext context, IFieldRepository<Field> repository)
         {
             _context = context;
             _repository = repository;
         }
        public async Task CreateField(Dictionary<string,int> fieldsType, Guid CollectionId)
        {
            System.Console.WriteLine("In createField method received:");
            foreach (var item in fieldsType)
            {
                System.Console.WriteLine(item.Key + " - " + item.Value);
            }
          
            var fields = new Fields() {CollectionId = CollectionId};
             _context.Fields.Add(fields);
             await _context.SaveChangesAsync();
            foreach (var item in fieldsType)
            {
                System.Console.WriteLine("In foreach!");
                var creation = item.Value switch
                {
                    1 => CreateTypedFieldAsync<FieldInt>(new FieldInt{Name = item.Key, FieldsId = fields.Id}),
                    2 => CreateTypedFieldAsync<FieldString>(new FieldString{Name = item.Key, FieldsId = fields.Id}),
                    3 => CreateTypedFieldAsync<FieldText>(new FieldText{Name = item.Key, FieldsId = fields.Id}),
                    4 => CreateTypedFieldAsync<FieldBool>(new FieldBool{Name = item.Key, FieldsId = fields.Id}),
                    5 => CreateTypedFieldAsync<FieldDate>(new FieldDate{Name = item.Key, FieldsId = fields.Id}),
                    _ => throw new ArgumentOutOfRangeException("Value must to be between 1 and 6 included")
        
                };
                
                creation.GetAwaiter().GetResult();
                System.Console.WriteLine(creation.IsCompletedSuccessfully);
            }
           
        
        
        }

        public async Task CreateTypedFieldAsync<T>(Field field)
        {
            if (field == null)
                throw new ArgumentNullException($"{nameof(field)} cann't be a null");

            await _context.AddAsync(field);
            await _context.SaveChangesAsync();
            System.Console.WriteLine("Was created {0} with type",field.Name, typeof(T));
        }
            
           
    }
}