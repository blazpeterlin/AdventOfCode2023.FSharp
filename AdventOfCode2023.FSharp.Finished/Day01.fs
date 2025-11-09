module Day01

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
    
    let nums = lns |> List.map grabNums |> List.transpose
    let nums0 = nums[0] |> List.sort
    let nums1 = nums[1] |> List.sort
    let numsSort = nums0::nums1::[] |> List.transpose

    let sumDist = numsSort |> List.map (fun (a::b::[]) -> abs(a-b)) |> List.sum



    let res = sumDist
    ClipboardService.SetText(res.ToString())
    res
 

let solve2 (inputPath: string) =
    let lns = File.ReadAllLines(dir + "/" + inputPath) |> Seq.filter((<>)"") |> Seq.toList;
    let nums = lns |> List.map grabNums |> List.transpose

    let nums0 = nums[0] |> List.sort
    let nums1 = nums[1] |> List.sort

    let count1 = nums1 |> List.countBy id |> Map.ofList

    let sumSimilarity = nums0 |> List.map (fun num -> if count1.ContainsKey num then num*count1[num] else 0 ) |> List.sum



    let res = sumSimilarity
    ClipboardService.SetText(res.ToString())
    res
