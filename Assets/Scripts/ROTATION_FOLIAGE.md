# 🔄 Rotation Automatique des Plantes

## ✨ NOUVELLE FONCTIONNALITÉ

**Le FoliagePainter applique automatiquement une rotation X de +89.98° !**

---

## 🎯 Pourquoi 89.98° sur X ?

### Le Problème

**Vos prefabs de plantes sont modélisés "debout" (vertical)** :

```
Prefab original :
   |
   |  ← Debout (comme un arbre)
   |
```

**Mais pour les plantes sous-marines (algues, coraux plats)**, vous voulez qu'elles soient **"couchées" sur le sol** :

```
Résultat souhaité :
____  ← À plat (comme sur le fond)
```

### La Solution

**Rotation X = 89.98°** fait pivoter le prefab pour le mettre presque à plat :

```
Avant :  |        Après : ____
        |                     
       (0°)              (89.98°)
```

**Presque 90°** (89.98° au lieu de 90°) évite les problèmes de gimbal lock et donne un aspect plus naturel.

---

## 🎨 Comment Ça Marche

### Rotation Complète Appliquée

**Quand vous peignez une plante** :

```
1. Rotation X : 89.98° (fixe)
   → Couche la plante

2. Rotation Y : aléatoire 0-360° (si Random Rotation activé)
   → Tourne autour de l'axe vertical

3. Rotation Z : 0° (fixe)
   → Pas d'inclinaison latérale
```

### Résultat Visuel

```
Plante 1 : (89.98°, 45°, 0)    → Couchée, orientée vers NE
Plante 2 : (89.98°, 180°, 0)   → Couchée, orientée vers S
Plante 3 : (89.98°, 270°, 0)   → Couchée, orientée vers W
Plante 4 : (89.98°, 92°, 0)    → Couchée, orientée vers E
...
```

**Toutes couchées, mais orientées différemment = Aspect naturel ! ✨**

---

## 🎛️ PARAMÈTRES

### Dans l'Inspector (FoliagePainter)

```
Rotation X: 89.98
→ Angle fixe appliqué sur l'axe X

Random Rotation: ✓
→ Rotation aléatoire sur Y (0-360°)
```

---

## 📊 CONFIGURATIONS

### Configuration 1 : Algues/Coraux Plats (DÉFAUT)

```
Rotation X: 89.98
Random Rotation: ✓
```

**Résultat** : Plantes couchées, orientations variées

---

### Configuration 2 : Plantes Debout

```
Rotation X: 0
Random Rotation: ✓
```

**Résultat** : Plantes verticales, rotations Y aléatoires

---

### Configuration 3 : Plantes Inclinées

```
Rotation X: 45
Random Rotation: ✓
```

**Résultat** : Plantes inclinées à 45°

---

### Configuration 4 : Plantes Alignées (Pas de Random)

```
Rotation X: 89.98
Random Rotation: ☐
```

**Résultat** : Toutes couchées dans la même direction

---

## 🔧 MODIFIER LA ROTATION

### Changer l'Angle X

**Si 89.98° ne convient pas** :

```
Rotation X: 90    → Complètement à plat
Rotation X: 80    → Légèrement relevé
Rotation X: 0     → Vertical (debout)
Rotation X: 45    → Incliné à 45°
Rotation X: -90   → À l'envers
```

### Désactiver la Rotation Aléatoire Y

```
Random Rotation: ☐
```

**Résultat** : Toutes les plantes ont exactement la même orientation

**Utile pour** : Créer des lignes, motifs, etc.

---

## 💡 ASTUCES

### Astuce 1 : Visualiser la Rotation

```
1. Peignez 1 plante
2. Sélectionnez-la dans la Hierarchy
3. Inspector → Transform → Rotation
4. Vous voyez : (89.98, random, 0)
```

### Astuce 2 : Tester Différents Angles

```
1. Peignez quelques plantes
2. Pas content ? CTRL + SHIFT + Clic pour effacer
3. Changez Rotation X
4. Repeignez
5. Comparez !
```

### Astuce 3 : Combiner Rotation et Scale

```
Scale Multiplier: 2
Rotation X: 89.98
Random Rotation: ✓
Scale Variation: 0.2

Résultat :
→ Plantes couchées x2 plus grandes
→ Orientations variées
→ Tailles légèrement différentes
→ Aspect super naturel ! 🌿✨
```

---

## 🐛 DÉPANNAGE

### "Mes plantes sont à l'envers !"

**Solution** :
```
Rotation X: 89.98 → Trop !
Essayez: -89.98 ou 0
```

### "Mes plantes sont debout au lieu d'être couchées"

**Solution** :
```
Rotation X: 0 → Pas bon
Changez à: 89.98
```

### "Toutes mes plantes ont la même orientation"

**Vérification** :
```
Random Rotation: ☐ → Décochez !
Changez à: ✓
```

### "Je veux des plantes verticales (coraux, grandes algues)"

**Solution** :
```
Rotation X: 0
Random Rotation: ✓
```

**Résultat** : Plantes debout avec rotation Y aléatoire

---

## 📐 COMPRENDRE LES AXES

### Axes Unity

```
Y (Haut/Bas)
  ↑
  |
  |____→ X (Droite/Gauche)
 /
Z (Avant/Arrière)
```

### Rotation X

**Fait pivoter autour de l'axe X (Rouge)** :

```
X = 0°     : | (debout)
X = 45°    : / (incliné)
X = 90°    : __ (couché)
X = -90°   : ‾‾ (à l'envers)
```

### Rotation Y

**Fait pivoter autour de l'axe Y (Vert)** :

```
Y = 0°     : Face Nord
Y = 90°    : Face Est
Y = 180°   : Face Sud
Y = 270°   : Face Ouest
```

**Random Rotation applique des valeurs aléatoires sur Y = orientation variée !**

---

## 🎊 RÉSUMÉ

**Configuration Actuelle** :
```
✅ Rotation X: 89.98° (plantes couchées)
✅ Random Rotation: ✓ (orientations variées)
✅ Prêt à peindre !
```

**Résultat** :
```
Chaque plante peinte :
  - Couchée sur le sol (X = 89.98°)
  - Orientée aléatoirement (Y = 0-360°)
  - Aspect naturel ! 🌿✨
```

---

## 🚀 TESTER MAINTENANT

```
1. Sélectionnez FoliagePainterMer
2. Vérifiez Inspector :
   - Rotation X: 89.98
   - Random Rotation: ✓
3. CTRL + Clic pour peindre
4. Vos plantes sont couchées avec orientations variées ! 🎨
```

**C'est automatique, pas besoin de toucher aux prefabs !** 🎉
