# ⚡ GUIDE EXPRESS : Changer le Shader (30 secondes)

## 🎯 EN 4 CLICS

### 1️⃣ Sélectionner l'Objet
```
Hierarchy → fond d'ocean_V1 (1) → CLIC
```

### 2️⃣ Voir les Materials
```
Inspector (droite) → Mesh Renderer → Materials → Déplier
```

### 3️⃣ Ouvrir un Material
```
Cliquez sur le NOM du material (ex: "Material1")
```

### 4️⃣ Changer le Shader
```
Inspector → Shader: ... → CLIC
Menu qui s'ouvre → Custom → URP Caustics Lit → CLIC
```

### ✅ Répéter
```
Faites ça pour CHAQUE material de la liste !
```

---

## 📍 Où Cliquer ?

```
┌─────────────┐
│  Hierarchy  │  ← 1️⃣ Cliquez sur fond d'ocean_V1 (1)
├─────────────┤
│ Items       │
│ ├─ Light    │
│ ├─ fond ✓   │  ✓ = sélectionné
│ └─ ...      │
└─────────────┘

┌─────────────────────────────┐
│      Inspector              │  
├─────────────────────────────┤
│ Mesh Renderer               │
│   Materials                 │  ← 2️⃣ Dépliez
│     Element 0: Material1    │  ← 3️⃣ Cliquez sur "Material1"
│     Element 1: Material2    │
└─────────────────────────────┘

┌─────────────────────────────┐
│      Inspector (Material)   │
├─────────────────────────────┤
│ Shader: Universal...        │  ← 4️⃣ Cliquez ici
│   Menu déroulant ▼          │
│     Custom                  │
│       URP Caustics Lit      │  ← 5️⃣ Cliquez ici
└─────────────────────────────┘
```

---

## ✨ Résultat

Après changement, vous verrez :
```
Shader: Custom/URP Caustics Lit  ✅
```

**C'est tout ! Répétez pour tous les materials ! 🎉**

---

## 🚀 Encore Plus Rapide ?

**Sélection multiple** :
```
1. Project → Trouvez vos materials
2. Ctrl + Clic (sélectionner plusieurs)
3. Inspector → Changez le Shader
4. Tous changent en même temps ! ⚡
```

---

**Documentation complète** : `TUTORIEL_APPLIQUER_SHADER.md`
