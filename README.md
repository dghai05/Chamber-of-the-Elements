# 🌪️ Chamber of the Elements

_A fantasy platformer built in Unity with combat, puzzles, and elemental progression._

## 🎮 Overview  
**Chamber of the Elements** is a third-person fantasy platformer where players traverse elemental-themed levels, solve environmental puzzles, and battle enemies using unlockable abilities. Built using Unity, the game combines real-time combat, ability-based progression, and interactive environments to deliver a multi-level adventure across Earth, Water, Fire, Air, and Ice.

---

## 🛠️ Technologies Used
- **Unity Engine** (Character Controller, UI System, Particle Systems, Animator)
- **C#** for scripting game mechanics
- **Cinemachine** for third-person camera control
- **Unity UI + TextMeshPro** for all in-game interface systems
- **Animator Controllers + Animation Events** for real-time synced combat
- **Custom Inheritance-based OOP System** for Items, Inventory, Characters, and Abilities
- **Singleton + Don'tDestroyOnLoad Patterns** for data persistence

---

## 🔑 Core Features

### 🧍 Character Customization & Progression
- Choose between male/female character appearances
- Player persists across scenes with unified stats and inventory
- Unlock elemental abilities per level (dash, double jump, shield, etc.)

### 🗡️ Real-Time Combat System
- Sword-based melee combat using animation events
- Enemies chase, idle, and attack based on proximity
- Hit detection, cooldowns, and defense-based damage handling

### 🍃 Abilities System
- **Dash**: evade and become temporarily invincible
- **Shield**: activate defense buff with cooldown UI
- **Double Jump**: available in air level
- Abilities unlock based on level progression and persist through scenes

### 🧪 Potion System
- Three potion types: Health, Shield, Speed
- Real-time usage via key inputs (1, 2, 3)
- Stackable inventory system with timed buffs and UI sync

### 🗂️ Inventory & Shop
- Modular inventory with support for stackable/non-stackable items
- Shop UI allows item purchases with coin validation
- Shop and player inventories persist across scenes

### 🌍 Multi-Level Design
Each elemental level features unique mechanics:
- **Earth**: parkour, puzzle interaction, shield-based boss fight  
- **Water/Ice**: dash-only traversal zones, spleef puzzles  
- **Air**: double-jump tutorial, memory bridge puzzle  
- **Fire**: platforming gauntlet and final boss encounter  
- **Main Hub**: totem-based progression and level access locking

### 🧠 Puzzle Mechanics
- Memory sequence challenges
- Environmental triggers (ruinstones, dash zones)
- Spleef platforms and safe zone navigation

### 🧑‍🤝‍🧑 NPC Interaction
- Trigger-based UI dialogue system with player response options
- Locks player movement during interaction
- Designed as reusable prefab with configurable responses

### 🎨 Visual FX & Atmosphere
- Custom fog, day/night skyboxes
- Snowfall via Unity Particle System
- Third-person camera using Cinemachine

---

## 🎓 Tutorial Flow
Each level introduces a core gameplay mechanic or ability:
- **Hub Scene**: Shop, Totem Pillars, Teleporters
- **Air Level**: teaches double jump  
- **Earth Level**: teaches shield usage  
- **Water/Ice Level**: teaches dash precision  
- **Fire Level**: final gauntlet and ultimate ability  

---

## 🚧 Future Improvements
- Implement a weapon switching system with matching animations
- Add mist/invisibility-based ability as originally scoped
- Polish death animations and knockback reactions
- Improve enemy pathfinding (NavMesh or FSM AI)
- Introduce a leveling/XP system for stat scaling
- Expand NPC dialogue trees with branching options
- Add cutscenes between level transitions

---
