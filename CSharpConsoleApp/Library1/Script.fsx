﻿open System.IO
type Observation = { Label: string; Pixels: int[] }

let toObservation (csvData:string) = 
    let columns = csvData.Split(',')
    let label = columns.[0]
    let pixels = columns.[1..] |> Array.map int
    { Label = label; Pixels = pixels }

let reader path = 
    let data = File.ReadAllLines path
    data.[1..]
    |> Array.map toObservation

let trainingPath = @"C:\Users\Alexander\Downloads\trainingsample.csv"
let trainingData = reader trainingPath

type Distance = int[] * int[] -> int
let manhattanDistance (pixels1,pixels2) =
    Array.zip pixels1 pixels2
    |> Array.map (fun (x,y) -> abs (x-y))
    |> Array.sum

let train (trainingset:Observation[]) = 
    let classify (pixels:int[]) =
        trainingset
        |> Array.minBy (fun x -> manhattanDistance(x.Pixels, pixels))
        |> fun x -> x.Label
    classify

let classifier = train trainingData

let validationPath = @"C:\Users\Alexander\Downloads\validationsample.csv"
let validationData = reader validationPath

validationData
|> Array.averageBy (fun x -> if classifier x.Pixels = x.Label then 1. else 0.)
|> printfn "Correct: %.3f"