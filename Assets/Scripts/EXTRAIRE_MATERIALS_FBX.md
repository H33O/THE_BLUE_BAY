# 🔓 DÉBLOQUER : Extraire les Materials d'un FBX

## 🎯 Votre Problème

**Symptômes** :
- ✅ Vous avez cliqué sur un material
- ❌ L'Inspector est tout grisé
- ❌ Vous ne pouvez rien modifier
- ❌ Le shader est verrouillé

**Cause** :
```
Le material est DANS le fichier FBX :
  Assets/3D_Assets/fond d'ocean_V1.fbx
    ├─ Model (mesh)
    └─ Materials (lecture seule !)  ← PROBLÈME
```

**Solution** :
```
Extraire les materials vers un dossier séparé
  → Les materials deviennent modifiables
  → Vous pouvez changer le shader !
```

---

## ✅ MÉTHODE 1 : Extraction Automatique (RECOMMANDÉ)

### ÉTAPE 1 : Sélectionner le Fichier FBX

```
1. Fenêtre "Project" (en bas)
2. Allez dans "Assets/3D_Assets"
3. Trouvez "fond d'ocean_V1.fbx"
4. Cliquez dessus UNE FOIS pour le sélectionner
```

**Visual** :
```
Project
├── Assets
│   ├── 3D_Assets
│   │   └── fond d'ocean_V1.fbx  ← CLIQUEZ ICI (fichier FBX)
```

**IMPORTANT** : Cliquez sur le **FICHIER FBX**, pas sur le material à l'intérieur !

### ÉTAPE 2 : Ouvrir l'Inspector du FBX

```
1. Inspector (à droite) s'affiche
2. Vous voyez plusieurs onglets : Model, Rig, Animation, Materials
3. Cliquez sur l'onglet "Materials"
```

**Visual** :
```
Inspector (du FBX)
┌─────────────────────────────────┐
│ fond d'ocean_V1.fbx Import      │
├─────────────────────────────────┤
│ [Model] [Rig] [Animation]       │
│ [Materials] ← CLIQUEZ ICI       │
├─────────────────────────────────┤
│ Materials                       │
│   Location: In Prefab Assets    │
│                                 │
│   Extract Materials...          │
│   Extract Textures...           │
└─────────────────────────────────┘
```

### ÉTAPE 3 : Extraire les Materials

```
1. Dans l'onglet Materials, cherchez le bouton "Extract Materials..."
2. Cliquez sur "Extract Materials..."
3. Unity ouvre une fenêtre de dialogue
4. Choisissez le dossier "Assets/Materials"
   (ou créez-le s'il n'existe pas)
5. Cliquez sur "Select Folder"
```

**Fenêtre qui s'ouvre** :
```
┌─────────────────────────────────┐
│ Select Folder                   │
├─────────────────────────────────┤
│ Assets                          │
│   ├── Materials  ← SÉLECTIONNEZ │
│   ├── Scripts                   │
│   └── 3D_Assets                 │
│                                 │
│         [Cancel] [Select Folder]│
└─────────────────────────────────┘
```

### ÉTAPE 4 : Attendre l'Extraction

```
Unity extrait les materials :
  - Crée des fichiers .mat dans Assets/Materials
  - Reconnecte automatiquement fond d'ocean_V1 (1)
  - Les materials sont maintenant modifiables !
```

**Résultat dans le Project** :
```
Assets
├── Materials (nouveau !)
│   ├── Material.001.mat  ← EXTRAIT ET MODIFIABLE
│   ├── Material.002.mat
│   └── Material.003.mat
├── 3D_Assets
│   └── fond d'ocean_V1.fbx
```

### ÉTAPE 5 : Modifier les Materials

Maintenant les materials ne sont PLUS grisés ! :

```
1. Project → Assets/Materials
2. Cliquez sur Material.001.mat
3. Inspector → Shader: ... (PLUS GRISÉ !)
4. Changez le shader → Custom/URP Caustics Lit ✅
```

---

## ✅ MÉTHODE 2 : Extraction Manuelle (Alternative)

Si le bouton "Extract Materials" ne fonctionne pas :

### Créer un Nouveau Material

```
1. Project → Assets/Materials (créez le dossier si besoin)
2. Clic droit → Create → Material
3. Nommez-le "OceanFloor_Caustics"
4. Shader → Custom/URP Caustics Lit
5. Configurez les couleurs/textures selon vos besoins
```

### Assigner le Nouveau Material

```
1. Hierarchy → fond d'ocean_V1 (1)
2. Inspector → Mesh Renderer → Materials
3. Pour chaque Element :
   - Glissez votre nouveau material "OceanFloor_Caustics"
   - OU cliquez sur le rond → sélectionnez le material
```

**Visual** :
```
Inspector (fond d'ocean_V1 (1))
├── Mesh Renderer
│   └── Materials
│       ├── Element 0: [○] Material.001 ← Clic sur le rond
│       │   └── Menu s'ouvre → OceanFloor_Caustics
│       ├── Element 1: [○] Material.002
│       └── Element 2: [○] Material.003
```

---

## 🎬 Guide Complet Étape par Étape

### Vue d'Ensemble

```
AVANT (materials verrouillés) :
  fond d'ocean_V1.fbx
    └── Materials (lecture seule)
        ├── Material.001 🔒
        ├── Material.002 🔒
        └── Material.003 🔒

EXTRACTION :
  Extract Materials... → Assets/Materials

APRÈS (materials modifiables) :
  Assets/Materials
    ├── Material.001.mat 🔓
    ├── Material.002.mat 🔓
    └── Material.003.mat 🔓
    
  fond d'ocean_V1 (1) (dans la scène)
    └── Utilise maintenant les .mat extraits
```

### Actions Détaillées

**1. Trouver le FBX** :
```
Project → Assets/3D_Assets → fond d'ocean_V1.fbx
CLIC sur le fichier FBX
```

**2. Aller dans Materials** :
```
Inspector → Onglet "Materials"
```

**3. Extraire** :
```
Bouton "Extract Materials..." → CLIC
Sélectionner "Assets/Materials"
Bouton "Select Folder" → CLIC
```

**4. Attendre** :
```
Unity process...
✅ Done!
```

**5. Vérifier** :
```
Project → Assets/Materials
Vous voyez les .mat extraits ✅
```

**6. Modifier** :
```
Cliquez sur Material.001.mat
Inspector n'est PLUS grisé !
Shader → Custom/URP Caustics Lit ✅
```

---

## ⚙️ Vérifications

### Comment Savoir si C'est Réussi ?

**AVANT l'extraction** :
```
Inspector (d'un material du FBX)
┌─────────────────────────────────┐
│ Material.001                    │
├─────────────────────────────────┤
│ Shader: Standard (grisé) 🔒     │ ← VERROUILLÉ
│ Main Maps (grisé)               │
│   Albedo (grisé)                │
└─────────────────────────────────┘
```

**APRÈS l'extraction** :
```
Inspector (du .mat extrait)
┌─────────────────────────────────┐
│ Material.001                    │
├─────────────────────────────────┤
│ Shader: Standard 🔓              │ ← MODIFIABLE
│ Main Maps                       │
│   Albedo                        │
│                                 │
│ [Vous pouvez tout modifier !]  │
└─────────────────────────────────┘
```

### Vérification dans le Project

```
Assets/Materials (doit contenir) :
  ✅ Material.001.mat
  ✅ Material.002.mat
  ✅ Material.003.mat
  ✅ (Tous vos materials extraits)
```

### Vérification dans l'Objet

```
Hierarchy → fond d'ocean_V1 (1)
Inspector → Mesh Renderer → Materials
  Element 0: Material.001  (Assets/Materials/Material.001.mat)
  Element 1: Material.002  (Assets/Materials/Material.002.mat)
  
Si vous voyez "(Assets/Materials/...)" → ✅ Extraction réussie !
Si vous voyez "(fond d'ocean_V1.fbx)" → ❌ Encore dans le FBX
```

---

## 🐛 Problèmes Courants

### "Je ne trouve pas le bouton Extract Materials"

**Vérifications** :
```
1. Vous avez bien cliqué sur le fichier .fbx (pas sur le material)
2. Vous êtes dans l'onglet "Materials" de l'Inspector
3. Unity version 2020+ (versions anciennes ont une autre méthode)
```

**Solution** :
```
Si vraiment absent :
1. Onglet Materials de l'Inspector
2. Cherchez "Location" → Changez en "Use External Materials (Legacy)"
3. Apply
4. Bouton "Extract Materials" apparaît
```

### "Extract Materials est grisé"

**Cause** : Déjà extrait ou pas de materials

**Vérification** :
```
Project → Cherchez les .mat
S'ils existent déjà dans Assets/Materials → Déjà fait !
```

### "Après extraction, je ne vois rien dans Assets/Materials"

**Solution** :
```
1. Project → Clic droit → Refresh (F5)
2. Regardez dans le dossier que vous avez sélectionné
3. Vérifiez que l'extraction a bien terminé
```

### "fond d'ocean_V1 (1) utilise toujours les materials du FBX"

**Solution** :
```
1. Sélectionnez fond d'ocean_V1 (1)
2. Inspector → Mesh Renderer → Materials
3. Pour chaque Element :
   - Clic droit → "Revert to Prefab"
   OU
   - Glissez manuellement les .mat extraits
```

---

## 💡 Avantages de l'Extraction

### AVANT (Materials dans FBX)

**Inconvénients** :
- ❌ Lecture seule
- ❌ Pas de modification possible
- ❌ Partagés avec tous les préfabs du même FBX
- ❌ Réimportation du FBX écrase les changements

### APRÈS (Materials Extraits)

**Avantages** :
- ✅ Modifiables à volonté
- ✅ Un material par objet (ou partagé si vous voulez)
- ✅ Indépendants du FBX
- ✅ Réimportation du FBX ne les touche pas
- ✅ Vous pouvez changer le shader !

---

## 🎯 Cas Spécial : Plusieurs Objets Utilisent le Même FBX

Si vous avez plusieurs objets qui utilisent `fond d'ocean_V1.fbx` :

**Après extraction** :
```
Tous les objets utiliseront LES MÊMES materials extraits

Scène :
  ├── fond d'ocean_V1 (1)  → Material.001.mat
  └── fond d'ocean_V1 (2)  → Material.001.mat (le même !)
```

**Avantage** :
```
Vous changez le shader une fois → Tous les objets changent !
```

**Si vous voulez des materials différents** :
```
1. Dupliquez les .mat extraits
2. Renommez-les (ex: Material.001_Zone1.mat)
3. Assignez-les individuellement à chaque objet
```

---

## 📋 Checklist Complète

### Extraction

```
[ ] 1. Project → Assets/3D_Assets
[ ] 2. Sélectionner fond d'ocean_V1.fbx (le fichier)
[ ] 3. Inspector → Onglet "Materials"
[ ] 4. Bouton "Extract Materials..."
[ ] 5. Sélectionner Assets/Materials
[ ] 6. Confirmer
[ ] 7. Attendre la fin du process
[ ] 8. Vérifier que les .mat sont dans Assets/Materials
```

### Modification

```
[ ] 1. Project → Assets/Materials
[ ] 2. Sélectionner Material.001.mat
[ ] 3. Vérifier que l'Inspector n'est PLUS grisé
[ ] 4. Shader → Custom/URP Caustics Lit
[ ] 5. Répéter pour tous les materials
```

### Test

```
[ ] 1. Hierarchy → fond d'ocean_V1 (1)
[ ] 2. Vérifier que les materials pointent vers Assets/Materials
[ ] 3. Play ▶️
[ ] 4. Descendre sous l'eau
[ ] 5. Voir les caustiques ✨
```

---

## 🎊 Résumé

**Problème** : Materials verrouillés (dans le FBX)

**Solution** : Extraire vers Assets/Materials

**Résultat** : Materials modifiables + Shader changeable !

**Temps** : 1 minute pour l'extraction + 2 minutes pour changer les shaders

**Une fois fait** : Plus jamais de problème ! Les materials sont à vous ! 🎉

---

## 🚀 Prochaines Étapes

Après avoir extrait les materials :

1. ✅ Changer les shaders → `Custom/URP Caustics Lit`
2. ✅ Ajuster les paramètres visuels si besoin
3. ✅ Tester en Play mode
4. ✅ Profiter des caustiques ! 🌊✨

**Vous êtes presque au but ! 💪**
