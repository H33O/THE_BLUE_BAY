# ⚡ DÉMARRAGE RAPIDE : Caustiques VFX (10 minutes)

## 🎯 Votre Objectif

Créer des caustiques sous-marines spectaculaires **SANS modifier vos materials FBX** ! ✨

---

## 📋 CHECKLIST EXPRESS

### ☐ ÉTAPE 1 : Installer VFX Graph (2 min)

```
Window → Package Manager
Recherchez "Visual Effect Graph"
Install → Attendez l'installation
```

✅ **Vérification** : Project → Clic droit → "Visual Effects" apparaît

---

### ☐ ÉTAPE 2 : Créer le VFX Asset (30 sec)

```
Project → Clic droit
Create → Visual Effects → Visual Effect Graph
Nom: "UnderwaterCaustics"
```

✅ **Vérification** : `UnderwaterCaustics.vfx` créé dans Project

---

### ☐ ÉTAPE 3 : Configuration Rapide du VFX (3 min)

**Ouvrez le VFX** :
```
Double-clic sur UnderwaterCaustics.vfx
```

**Blackboard (gauche) - Ajoutez les propriétés** :

Cliquez sur "+" et créez :

1. `Intensity` (Float) - Default: 1.0 - Exposée ✓
2. `AnimationSpeed` (Float) - Default: 0.5 - Exposée ✓
3. `Scale` (Float) - Default: 1.5 - Exposée ✓
4. `CausticsColor` (Vector4) - Default: (0.4, 0.8, 1, 1) - Exposée ✓
5. `WaterLevel` (Float) - Default: 0 - Exposée ✓
6. `EffectDepth` (Float) - Default: 50 - Exposée ✓

**Configuration Minimale du Graph** :

**Initialize Particle** :
```
- Capacity: 3000
- Set Lifetime Random: Min 2, Max 4
- Set Velocity Random: Min (-0.1, -1, -0.1), Max (0.1, -0.5, 0.1)
- Set Position: Shape Cylinder
  - Center: (0, 0, 0)
  - Radius: 25
  - Height: 5
- Set Size: 0.5
- Set Color: CausticsColor
```

**Update Particle** :
```
- Turbulence: Intensity 0.2
```

**Output Particle Quad** :
```
- Blend Mode: Additive
- Orient: Face Camera Position
```

**Sauvegardez** : Ctrl + S

✅ **Vérification** : Graph configuré sans erreurs

---

### ☐ ÉTAPE 4 : Placer dans la Scène (1 min)

```
Hierarchy → Clic droit
Visual Effects → Visual Effect
Nom: "Underwater Caustics VFX"

Transform:
  Position: (0, 0, 0)
  Rotation: (0, 0, 0)
  Scale: (1, 1, 1)
```

**Assigner le VFX** :
```
Inspector → Visual Effect
Asset Template → Glissez UnderwaterCaustics.vfx
```

✅ **Vérification** : Asset Template assigné

---

### ☐ ÉTAPE 5 : Ajouter le Contrôleur (1 min)

**Sur "Underwater Caustics VFX"** :
```
Inspector → Add Component
Cherchez "VFXCausticsController"
Ajoutez-le
```

**Configuration** :
```
VFX Caustics Controller:
  ├── Caustics VFX: (auto-référencé)
  ├── Water Level: 0
  ├── Effect Depth: 50
  ├── Intensity: 1
  ├── Animation Speed: 0.5
  ├── Scale: 1.5
  ├── Caustics Color: (0.4, 0.8, 1, 1)
  └── Enable Caustics: ✓
```

✅ **Vérification** : Composant ajouté et configuré

---

### ☐ ÉTAPE 6 : Connecter au Système Underwater (1 min)

```
Hierarchy → HoverCar/Main Camera
Inspector → UnderwaterEffectController
Section "Caustiques VFX":
  VFX Caustics Controller → Glissez "Underwater Caustics VFX"
```

✅ **Vérification** : Lien assigné dans l'Inspector

---

### ☐ ÉTAPE 7 : TESTER ! (30 sec)

```
1. Sauvegardez (Ctrl + S)
2. Play ▶️
3. Descendez sous l'eau (Y < 0)
4. Les caustiques apparaissent ! ✨
```

✅ **Vérification** : Caustiques visibles sous l'eau

---

## 🎨 VERSION ENCORE PLUS RAPIDE (5 min)

Si vous voulez juste tester :

### Configuration Minimale du VFX Graph

**Ouvrez UnderwaterCaustics.vfx**, ajoutez SEULEMENT :

**Blackboard** :
```
Intensity (Float, Exposed, Default: 1)
```

**Graph** :
```
Spawn → Rate: 500
Initialize → Capacity: 2000
Output → Blend Mode: Additive
```

**C'est tout !** Passez aux étapes 4-7.

Vous ajusterez les détails plus tard.

---

## 🐛 SI ÇA NE MARCHE PAS

### Pas de particules visibles ?

**Checklist** :
```
[ ] VFX Graph package installé
[ ] UnderwaterCaustics.vfx assigné dans Visual Effect
[ ] VFXCausticsController ajouté
[ ] Intensity > 0
[ ] Enable Caustics ✓
[ ] Play mode actif
[ ] Vous êtes sous l'eau (Y < 0)
```

### Particules roses ?

```
VFX Graph → Output → Utilisez le shader par défaut
```

### Effet ne s'active pas ?

```
Main Camera → UnderwaterEffectController
VFX Caustics Controller doit être assigné
```

---

## 🎛️ AJUSTEMENTS RAPIDES

### Trop faible ?
```
Underwater Caustics VFX → Intensity → 1.5
```

### Trop fort ?
```
Underwater Caustics VFX → Intensity → 0.5
```

### Animation trop rapide ?
```
Underwater Caustics VFX → Animation Speed → 0.2
```

### Couleur différente ?
```
Underwater Caustics VFX → Caustics Color → Changez
```

---

## ✅ RÉSULTAT FINAL

**Vous avez maintenant** :

```
✨ Caustiques VFX spectaculaires
✅ Activation automatique sous l'eau
✅ Zéro modification de materials
✅ Contrôle facile par niveau Y
✅ Fonctionne sur fond d'ocean_V1 (1)
```

**Sans avoir touché à vos materials FBX !** 🎉

---

## 📖 POUR ALLER PLUS LOIN

- **Configuration avancée** : `CREER_VFX_CAUSTIQUES.md`
- **Installation détaillée** : `INSTALLER_VFX_GRAPH.md`
- **Migration shader** : `MIGRATION_SHADER_VERS_VFX.md`

---

## 🎊 C'EST FINI !

**Total : 10 minutes**

**Profitez de vos caustiques sous-marines ! 🌊✨**
