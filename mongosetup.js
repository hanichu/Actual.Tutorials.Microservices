conn = new Mongo();

var uids = [ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId(), ObjectId()];

db = conn.getDB("orders-db");
db.orders.remove({});
db.orders.insertOne({ _id: ObjectId(), address: "Viale Felissent, 20/D - Fontane di Villorba, Treviso", items: [{ sku: uids[0], quantity: 1 }, { sku: uids[2], quantity: 5 }, { sku: uids[4], quantity: 12 }]});
db.orders.insertOne({ _id: ObjectId(), address: "Viale Felissent, 20/D - Fontane di Villorba, Treviso", items: [{ sku: uids[1], quantity: 3 }] });
db.orders.insertOne({ _id: ObjectId(), address: "Viale Felissent, 20/D - Fontane di Villorba, Treviso", items: [{ sku: uids[1], quantity: 12 }, { sku: uids[2], quantity: 11 }, { sku: uids[5], quantity: 5 }] });
db.orders.insertOne({ _id: ObjectId(), address: "Vis Bassa, 62 - S. Trovaso, Treviso", items: [{ sku: uids[0], quantity: 14 }, { sku: uids[3], quantity: 34 }, { sku: uids[6], quantity: 7 }] });
db.orders.insertOne({ _id: ObjectId(), address: "Vis Bassa, 62 - S. Trovaso, Treviso", items: [{ sku: uids[1], quantity: 1 }] });
db.orders.insertOne({ _id: ObjectId(), address: "Vis Bassa, 62 - S. Trovaso, Treviso", items: [{ sku: uids[1], quantity: 12 }, { sku: uids[2], quantity: 1 }, { sku: uids[5], quantity: 5 }] });
db.orders.find({});

db = conn.getDB("supplies-db");
db.products.remove({});
db.products.insertOne({ _id: uids[0], description: 'TV Led Sony 43" 4K', availability: Math.round(Math.random() * 100), categories: ['TV','elettronica','Home-Teather'] });
db.products.insertOne({ _id: uids[1], description: 'TV Led Samsung 54" 4K', availability: Math.round(Math.random() * 100), categories: ['TV', 'elettronica', 'Home-Teather'] });
db.products.insertOne({ _id: uids[2], description: 'Monitor LG 32" Full HD', availability: Math.round(Math.random() * 500), categories: ['Monitor', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[3], description: 'Monitor DELL 27" 4K', availability: Math.round(Math.random() * 500), categories: ['Monitor', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[4], description: 'Tastiera Wireless Logitech 54 Tasti bluetooth', availability: Math.round(Math.random() * 2000), categories: ['Tastiere', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[5], description: 'Tastiera Wireless Samsung bluetooth', availability: Math.round(Math.random() * 2000), categories: ['Tastiere', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[6], description: 'Laptop Dell Inspire 13.3" I9 octacore', availability: Math.round(Math.random() * 1000), categories: ['Computer', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[7], description: 'Laptop Dell Inspire 15.4" I9 octacore', availability: Math.round(Math.random() * 1000), categories: ['Computer', 'elettronica', 'PC'] });
db.products.insertOne({ _id: uids[8], description: 'Battery pack 9 cell compatibile DELL', availability: Math.round(Math.random() * 3000), categories: ['Batterie', 'elettronica', 'PC'] });
db.products.find({});
