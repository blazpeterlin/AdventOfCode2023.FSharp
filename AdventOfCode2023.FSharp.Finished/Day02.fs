module Day02

open Common
open System
open System.Collections.Generic
open Microsoft.FSharp.Core.Operators.Checked
open Common.Operators
open System.IO
open TextCopy

let dir = __SOURCE_DIRECTORY__

//type Present = { X: int; Y: int; Z: int; }

//let parseLn (ln:string) =
//    match ln with
//    | Regex @"^(\w+) blabla (\d+)$" [a; Int b] ->
//        (a,b)
        


let solve1 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;
    
    let reps = lns |> List.map grabNums
    let diffs = reps |> List.map (fun lst -> lst |> List.windowed 2 |> List.map (fun (x::y::[]) -> y-x))
    let goodDiffs = 
        diffs 
        |> List.filter (fun lst -> lst |> List.forall(fun x -> x > 0) || lst |> List.forall(fun x -> x < 0))
        |> List.filter (List.forall (fun x -> (abs x) <= 3))





    let res = goodDiffs |> List.length
    ClipboardService.SetText(res.ToString())
    res
 

let solve2 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;

    let reps = lns |> List.map grabNums
    let diffs = 
        reps
        |> List.indexed
        |> List.map(
            fun (repIdx: int, rep: int list) -> 
                [-1..rep.Length-1]
                |> List.map (
                    fun filterIdx ->
                        let filteredRep = rep |> List.indexed |> List.filter (fun (idx,num) -> idx<>filterIdx) |> List.map snd
                        let frDiffd = filteredRep|> List.windowed 2 |> List.map (fun (x::y::[]) -> y-x)
                        (repIdx, frDiffd)
                )
         )
         |> List.concat
        
    let goodDiffs = 
          diffs 
          |> List.filter (fun (idx, lst) -> lst |> List.forall(fun x -> x > 0) || lst |> List.forall(fun x -> x < 0))
          |> List.filter (fun (idx, lst) -> lst |> List.forall (fun x -> (abs x) <= 3))
          |> List.map fst
          |> List.distinct





    let res = goodDiffs |> List.length
    ClipboardService.SetText(res.ToString())
    res
    0
