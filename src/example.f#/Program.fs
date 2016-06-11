open System
open tesla

[<EntryPoint>]
let main argv = 
    let answer = Thing().Get(42)
    printfn "The answer is %d." answer
    0
