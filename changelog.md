# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0-preview.3](https://github.com/unity-game-framework/ugf-pool/releases/tag/2.0.0-preview.3) - 2022-05-15  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-pool/milestone/5?closed=1)  
    

### Added

- Add dynamic collection public build and destroy methods ([#14](https://github.com/unity-game-framework/ugf-pool/issues/14))  
    - Add `PoolCollectionDynamic<T>.Build()` and `Destroy()` methods to build and destroy single items.
- Add collection events ([#12](https://github.com/unity-game-framework/ugf-pool/issues/12))  
    - Add `IPoolCollection` and `IPoolCollection<T>` events such as `Added`, `Removed`, `Cleared`, `ItemEnabled` and `ItemDisabled` with implementation for `PoolCollection<T>` class.

### Fixed

- Fix disable execute for added item ([#13](https://github.com/unity-game-framework/ugf-pool/issues/13))  
    - Fix `PoolCollection.Add()` method to execute `IPoolObject.PoolDisable()` when possible.

## [2.0.0-preview.2](https://github.com/unity-game-framework/ugf-pool/releases/tag/2.0.0-preview.2) - 2022-05-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-pool/milestone/4?closed=1)  
    

### Added

- Add unity object dynamic collections ([#10](https://github.com/unity-game-framework/ugf-pool/issues/10))  
    - Add `PoolCollectionDynamicComponent<T>` class as implementation of dynamic collection to work with component attached to gameobject.
    - Add `PoolCollectionDynamicObject<T>` class as implementation of dynamic collection to work with _Unity_ objects.
    - Add `PoolCollectionDynamicScriptableObject<T>` class as implementation of dynamic collection to work with _ScriptableObject_ classes.
    - Change `PoolComponent` class namespace to `UGF.Pool.Runtime.Unity`.
    - Remove `GameObjectPoolCollection<T>` class, use `PoolCollectionDynamicComponent<T>` instead.

## [2.0.0-preview.1](https://github.com/unity-game-framework/ugf-pool/releases/tag/2.0.0-preview.1) - 2022-05-13  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-pool/milestone/3?closed=1)  
    

### Changed

- Add dynamic collection to have build and destroy handlers ([#8](https://github.com/unity-game-framework/ugf-pool/issues/8))  
    - Add `PoolCollectionDynamicHandlers<T>` class as implementation of dynamic pool with build and destroy handlers.
    - Change `PoolCollectionDynamic<T>` class to be abstract and have to implement `OnBuild()` and `OnDestroy()` abstract methods to manage item creation.
    - Change `GameObjectPoolCollection` class to use updated `PoolCollectionDynamic<T>` class with source object.
    - Change `GameObjectPoolBehaviour` class to `PoolComponent` and moved to upper namespace.
    - Remove `GameObjectPoolCollectionCycle` class, use `PoolCollectionCycle` class instead.

## [2.0.0-preview](https://github.com/unity-game-framework/ugf-pool/releases/tag/2.0.0-preview) - 2022-05-08  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-pool/milestone/2?closed=1)  
    

### Changed

- Rework and update to latest packages ([#6](https://github.com/unity-game-framework/ugf-pool/issues/6))  
    - Update dependencies: add `com.ugf.runtimetools` of `2.7.0` version and remove `com.ugf.builder` package.
    - Update package _Unity_ version to `2021.3`.
    - Update package _API Compatibility_ to `.NET Standard 2.1`.
    - Rework all collections.

## [1.0.0](https://github.com/unity-game-framework/ugf-pool/releases/tag/1.0.0) - 2019-03-20  

- [Commits](https://github.com/unity-game-framework/ugf-pool/compare/86be6be...1.0.0)
- [Milestone](https://github.com/unity-game-framework/ugf-pool/milestone/1?closed=1)

### Added
- This is a initial release.


