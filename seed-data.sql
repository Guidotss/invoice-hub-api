INSERT INTO "Statuses" ("Id", "Name", "CreatedAt", "UpdatedAt") 
VALUES 
    (gen_random_uuid(), 'uploaded', NOW(), NOW()),
    (gen_random_uuid(), 'processing', NOW(), NOW()),
    (gen_random_uuid(), 'validated', NOW(), NOW()),
    (gen_random_uuid(), 'failed', NOW(), NOW());


INSERT INTO "Taxes" ("TaxType", "CreatedAt", "UpdatedAt") 
VALUES 
    ('CUIT', NOW(), NOW()),
    ('NIF', NOW(), NOW());

