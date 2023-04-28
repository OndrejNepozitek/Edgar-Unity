---
title: FAQ
---

import TOCInline from '@theme/TOCInline';
import TrackGeneratorProgress from './faq/_track-generator-progress.md';
import EndlesslevelGeneration from './faq/_endless-level-generation.md';
import LevelsWithoutCorridors from './faq/_levels-without-corridors.md';
import LocksAndKeys from './faq/_locks-and-keys.md';
import NonRectangularRooms from './faq/_non_rectangular_rooms.md';
import SaveAndLoad from './faq/_save-and-load.md';
import ErrorInBuild from './faq/_error-in-build.md';
import Pathfinding from './faq/_pathfinding.md';
import Enemies from './faq/_enemies.md';
import PlayerSpawn from './faq/_player-spawn.md';
import Timeout from './faq/_timeout.md';
import WiderWalls from './faq/_wider-walls.md';
import RoomTemplateChangesLost from './faq/_room-template-changes-lost.md';
import RoomDistance from './faq/_room-distance.md';
import MultiplayerSeed from './faq/_multiplayer-seed.md';

This document contains solutions to common questions that are often asked on Discord and other channels.

#### Table of Contents

<TOCInline toc={toc} maxHeadingLevel={2} />

## How to spawn the player in a specific room?

<PlayerSpawn />

## What to do with a `TimeoutException`?

<Timeout />

## How to deal with wider walls/doors?

<WiderWalls />

## Changes to a room template are lost after a level is generated

<RoomTemplateChangesLost />

## Rooms are generated too close to one another

<RoomDistance />

## Send the same level to multiple players (in a multiplayer game)

<MultiplayerSeed />

## Keep prefab references when generating levels inside the editor

There is a dedicated guide [here](../recipes/prefabs-in-editor.md).

## Is it possible to track the progress of the generator?

<TrackGeneratorProgress />

## Is Edgar suitable for an endless level generation?

<EndlesslevelGeneration />

## How to generate levels without corridors?

<LevelsWithoutCorridors />

## TODO How to implement locks and keys?

<LocksAndKeys />

## Is it possible to have non-rectangular rooms?

<NonRectangularRooms />

## How to implement a save and load system?

<SaveAndLoad />

## The generator works in the editor but not in the build

<ErrorInBuild />

## How to handle pathfinding?

<Pathfinding />

## TODO: How to spawn enemies?

<Enemies />