# ⚡ DÉMARRAGE RAPIDE - NOUVEAU SYSTÈME DE CAUSTIQUES

## 🎯 EN 3 ÉTAPES

### ✅ ÉTAPE 1 : Appliquer le Material au Sol (1 minute)

```
1. Project → Assets/Materials → GroundWithCaustics
2. Hierarchy → sol
3. Glissez le material dans Inspector → Mesh Renderer → Materials
```

### ✅ ÉTAPE 2 : Tester (30 secondes)

```
1. Play ▶️
2. Descendez sous l'eau (Y < 0)  
3. Regardez le sol → Caustiques apparaissent ! ✨
```

### ✅ ÉTAPE 3 : C'est Tout !

**Le système fonctionne maintenant ! 🎉**

---

## 🔧 Si Besoin d'Ajustements

### Rendre les Caustiques Plus Fortes

```
Hierarchy → Global Caustics
Inspector → Caustics Intensity → 1.0
```

### Changer la Vitesse d'Animation

```
Global Caustics → Caustics Speed → 0.5 (plus rapide)
```

### Appliquer à D'Autres Objets

```
1. Sélectionnez l'objet
2. Inspector → Material → Shader → Custom/URP Caustics Lit
```

---

## ✨ Comment Ça Marche

**Automatique** :
- Au-dessus de l'eau → Pas de caustiques
- Sous l'eau → Caustiques activées
- Transition fluide (1 seconde)

**Objets concernés** :
- Seulement ceux avec shader `Custom/URP Caustics Lit`
- Le sol a déjà le material prêt (`GroundWithCaustics`)

---

## 📖 Documentation Complète

- **Nouveau système** : `NOUVEAU_SYSTEME_CAUSTICS.md`
- **Ancienne doc** : Ignorez les fichiers `FIX_Caustics_*.md` (obsolètes)

---

## 🎊 Profitez !

Votre système de caustiques fonctionne enfin ! 🌊✨
