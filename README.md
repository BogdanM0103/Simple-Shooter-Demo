# Simple Shooter Demo 🎯

A simple first-person shooter built in **Unity**.  
Move around the map, pick up weapons and ammo packs, and battle against enemies and turrets until you either win the game or die.

---

## 🚀 Features

- **Player Movement**  
  - Built using **StarterAssetsInputs** for smooth character control.
  - Supports mouse look and WASD movement.

- **Enemies**  
  - Use **NavMesh Surfaces** for intelligent navigation.  
  - No matter where they spawn, they will find a path to the player.

- **Turrets**  
  - Automatically rotate to face the player’s position.  
  - Fire projectiles at the player.

- **Weapons & Pickups**  
  - Weapons are defined via **ScriptableObjects** for easy balancing and configuration.  
  - **Base Pickup Class** used for both weapons and ammo packs, making item logic reusable and consistent.  
  - Player can switch weapons when picked up.

- **Game Flow**  
  - The game ends when:
    - The player dies ❌  
    - OR all enemies are eliminated ✅

---

## 🛠️ Tech Stack

- **Engine**: Unity (with Starter Assets)  
- **AI**: Unity NavMesh  
- **Programming Language**: C#

---

## 📸 Gameplay Overview

- Run and shoot enemies.  
- Collect ammo and weapons.  
- Defeat all enemies and turrets to win.  
- Survive as long as possible.

---

## 📂 Project Structure

- `Scripts/`
  - `Player/` → Player controls, input handling.  
  - `Enemies/` → Enemy AI & navigation.  
  - `Turrets/` → Turret tracking and shooting.  
  - `Weapons/` → ScriptableObjects, weapon logic, pickups.  
  - `GameManager/` → Handles win/lose conditions.
