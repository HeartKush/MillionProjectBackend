// MongoDB initialization script
db = db.getSiblingDB("PropertiesBD");

// Create collections
db.createCollection("properties");
db.createCollection("owners");
db.createCollection("propertytraces");

// Create indexes for better performance
db.properties.createIndex({ idProperty: 1 }, { unique: true });
db.properties.createIndex({ idOwner: 1 });
db.properties.createIndex({ address: "text", description: "text" });

db.owners.createIndex({ idOwner: 1 }, { unique: true });
db.owners.createIndex({ name: "text" });

db.propertytraces.createIndex({ idPropertyTrace: 1 }, { unique: true });
db.propertytraces.createIndex({ idProperty: 1 });
db.propertytraces.createIndex({ dateSale: 1 });

print("MongoDB initialization completed successfully");
