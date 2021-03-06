module TodosLib.TodoInMemory

open TodosLib
open Microsoft.Extensions.DependencyInjection
open System.Collections

let find(inMemory : Hashtable) (criteria : TodoCriteria) : Todo[] =
    match criteria with
        | All -> inMemory.Values |> Seq.cast |> Array.ofSeq
        
let save (inMemory : Hashtable) (todo : Todo) : Todo =
    inMemory.Add(todo.id, todo) |> ignore
    todo

let delete(inMemory : Hashtable) (id : string) : bool =
    inMemory.Remove(id)
    if inMemory.[id] = null then true
    else false
    
    
type IServiceCollection with
  member this.AddTodoInMemory (inMemory : Hashtable) =
    this.AddSingleton<TodoFind>(find inMemory) |> ignore
    this.AddSingleton<TodoSave>(save inMemory) |> ignore
    this.AddSingleton<TodoDelete>(delete inMemory) |> ignore