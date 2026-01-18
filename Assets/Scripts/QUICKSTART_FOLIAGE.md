# ⚡ DÉMARRAGE RAPIDE : Peindre des Plantes (3 minutes)

## 🎯 But : Peindre vos plantes sous-marines avec un pinceau

---

## ✅ ÉTAPE 1 : Tag "Foliage" (30 sec)

```
Menu → Edit → Project Settings
Tags and Layers → Tags → "+"
Nom: Foliage
Close
```

---

## ✅ ÉTAPE 2 : Créer Prefabs (1 min)

**Trouvez vos plantes** :
```
Project → Pandazole_Ultimate_Pack/.../Models ou Prefabs
```

**Créez 3-5 prefabs pour tester** :
```
1. Glissez une plante (ex: Coral_16) dans la scène
2. Hierarchy → Coral_16 → Tag: Foliage
3. Glissez de Hierarchy → Project
4. Prefab créé ! Supprimez l'instance
5. Répétez pour 2-3 autres plantes
```

---

## ✅ ÉTAPE 3 : Créer Foliage Painter (30 sec)

```
Hierarchy → Clic droit → Create Empty
Nom: "Foliage Painter"

Inspector → Add Component → "FoliagePainter"
```

---

## ✅ ÉTAPE 4 : Assigner Prefabs (30 sec)

```
Inspector (Foliage Painter)

Foliage Prefabs:
  Size: 3
  Element 0: [Glissez votre prefab 1]
  Element 1: [Glissez votre prefab 2]
  Element 2: [Glissez votre prefab 3]

Brush Size: 10
Density: 10
Scale Multiplier: 2    ← Multiplie la taille x2 !
Rotation X: 89.98      ← Couche les plantes sur le sol !
Random Rotation: ✓     ← Orientations variées !
```

**Cliquez** : `[Créer Dossier Parent]`

---

## ✅ ÉTAPE 5 : Ajouter Collider sur le Terrain (30 sec)

**IMPORTANT** : Le terrain doit avoir un collider !

```
Hierarchy → fond d'ocean_V1 (1)
Inspector → Add Component → "Mesh Collider"
```

---

## 🎨 ÉTAPE 6 : PEINDRE ! (30 sec)

```
1. Sélectionnez "Foliage Painter" dans Hierarchy
2. Vue Scene (pas Game)
3. CTRL + Clic gauche sur le fond d'océan
4. → Plantes apparaissent ! 🌿✨
5. CTRL + Glisser pour peindre en continu
```

**Cercle vert** = Zone de peinture

---

## 🎛️ AJUSTEMENTS RAPIDES

**Plus grosses plantes** :
```
Scale Multiplier: 3  ← Triple la taille !
```

**Plantes plus petites** :
```
Scale Multiplier: 1  ← Taille normale
Scale Multiplier: 0.5  ← Moitié de la taille
```

**Plus de plantes par clic** :
```
Density: 20
```

**Zone plus large** :
```
Brush Size: 20
```

**Effacer** :
```
CTRL + SHIFT + Clic
```

---

## ✅ CHECKLIST

```
[ ] Tag "Foliage" créé
[ ] 3 prefabs créés et taggés
[ ] Foliage Painter créé
[ ] Prefabs assignés
[ ] Mesh Collider sur le terrain
[ ] CTRL + Clic pour peindre
[ ] Ça marche ! 🎉
```

---

## 🎊 RÉSULTAT

**Vous pouvez maintenant** :
- ✨ Peindre des plantes avec CTRL + Clic
- 🎨 Ajuster la taille et densité en temps réel
- 🗑️ Effacer avec CTRL + SHIFT + Clic
- 🌿 Décorer tout votre fond marin !

**Pour les détails** : Voir `GUIDE_FOLIAGE_PAINTER.md`

**Temps total : 3 minutes** ⚡
