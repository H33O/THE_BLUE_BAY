# ⚡ GUIDE RAPIDE : Appliquer les Caustiques à votre Fond d'Océan

## 🎯 Votre Situation

- Objet : `fond d'ocean_V1 (1)`
- Problème : Plusieurs materials
- Besoin : Caustiques uniquement sous Y = 0

## ✅ Solution en 3 Étapes (2 minutes)

### ÉTAPE 1 : Ouvrir l'Objet (10 secondes)

```
Hierarchy → fond d'ocean_V1 (1)
Cliquez dessus
```

### ÉTAPE 2 : Changer le Shader des Materials (1 minute)

```
Inspector → Mesh Renderer → Materials
```

**Pour CHAQUE material dans la liste** :

1. Cliquez sur le nom du material (ouvre l'Inspector du material)
2. Dans l'Inspector du material :
   - **Shader** → Cherchez "Custom"
   - Sélectionnez **Custom/URP Caustics Lit**
3. Répétez pour tous les materials

**Alternative Rapide** :
- Sélectionnez tous les materials dans le Project
- Dans l'Inspector, changez le Shader en masse

### ÉTAPE 3 : Tester (30 secondes)

```
1. Play ▶️
2. Regardez fond d'ocean_V1 (1)
3. Les caustiques apparaissent uniquement sur la partie sous Y=0 ✨
```

---

## 🎛️ Ajustements (Optionnel)

### Si Votre Surface d'Eau N'est PAS à Y = 0

```
Hierarchy → Global Caustics
Inspector → Water Level → [Votre niveau d'eau]

Exemples :
  - Eau à Y = 5  → Water Level = 5
  - Eau à Y = -3 → Water Level = -3
```

### Si les Caustiques Sont Trop Faibles

```
Global Caustics → Caustics Intensity → 1.0
```

### Si la Transition Est Trop Brutale

```
Global Caustics → Fade Distance → 5
```

---

## ✅ Résultat Attendu

**Au-dessus de Y = 0** :
- Pas de caustiques sur le fond

**En-dessous de Y = 0** :
- Caustiques visibles et animées
- Fade progressif de Y=0 à Y=-2

**Transition automatique** :
- Y = 0 → 0% caustiques
- Y = -1 → 50% caustiques  
- Y = -2 → 100% caustiques

---

## 🐛 Si Ça Ne Marche Pas

### Vérification 1 : Shader Appliqué ?

```
Hierarchy → fond d'ocean_V1 (1)
Inspector → Materials → Shader doit être "Custom/URP Caustics Lit"
```

### Vérification 2 : Position Correcte ?

```
Hierarchy → fond d'ocean_V1 (1)
Inspector → Transform → Position Y
```

Si Position Y > Water Level → Pas de caustiques (normal)

### Vérification 3 : Intensité Active ?

```
Hierarchy → Global Caustics
Inspector → Caustics Intensity > 0
```

Descendez sous l'eau avec le HoverCar pour activer

---

## 📖 Documentation Complète

- **Solution détaillée** : `SOLUTION_CAUSTIQUES_PAR_Y.md`
- **Système général** : `NOUVEAU_SYSTEME_CAUSTICS.md`

---

## 🎊 C'est Tout !

Les caustiques apparaissent maintenant **uniquement sous le niveau Y** que vous définissez ! 🌊✨

**Fonctionne avec plusieurs materials sans problème !**
