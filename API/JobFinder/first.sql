/* Formatted on 04/08/2020 09:36:42 (QP5 v5.287) */
FUNCTION get_barcodes_by_mm_id (p_mm_id     IN t_id,
                                p_barcode   IN t_name,
                                p_count     IN t_id)
   RETURN t_id
IS
   v_count    t_id;
   o_result   SYS_REFCURSOR;
   t_out_ob   loc_out_bufor%ROWTYPE;
   t_amount   t_id;
   v_mag      t_id;
   v_count1   t_id;
   
BEGIN
   SELECT "mag"
     INTO v_mag
     FROM "gm_obroty"
    WHERE "id" = p_mm_id;

   v_count := 0;

   IF (v_mag <> 99)
   THEN
      FOR ob
         IN (  SELECT m."kkreskowy" AS ProductBarCode,
                      SUM (D."ilosc") AS AMOUNT
                 FROM "gm_mater" parent,
                      "gm_mater_sz" child,
                      "gm_karty" d,
                      "gm_kody_kreskowe" m
                WHERE     d."id_gm_obroty" = p_mm_id
                      AND child."id" = d."id_gm_mater_sz"
                      AND parent."id" = d."id_gm_mater"
                      AND TRIM (m."symbol") = TRIM (parent."symbol")
                      AND (   m."numer" = child."opis2"
                           OR m."numer" = child."numer"
                           OR m."numer" = child."opis1")
                      AND m."kkreskowy" = p_barcode
             GROUP BY m."kkreskowy")
      LOOP
         SELECT COUNT (*)
           INTO v_count1
           FROM loc_out_bufor
          WHERE     OB.ProductBarCode = LOCO_PR_BARCODE
                AND p_mm_id = LOCO_SCHPT_ID;

         IF v_count1 = 0
         THEN
            t_amount := p_count;
         ELSE
            SELECT SUM (loco_pr_amount)
              INTO t_amount
              FROM loc_out_bufor
             WHERE     OB.ProductBarCode = LOCO_PR_BARCODE
                   AND p_mm_id = LOCO_SCHPT_ID;

            t_amount := t_amount + p_count;
         END IF;

         IF (t_amount <= OB.AMOUNT)
         THEN
            v_count := 1;
         ELSE
            v_count := 0;
         END IF;
      END LOOP;
   ELSE
      FOR ob
         IN (  SELECT m."kkreskowy" AS ProductBarCode,
                      SUM (D."ilosc") AS AMOUNT
                 FROM "gm_karty" d
                      JOIN "gm_mater_sz" child
                         ON child."id" = d."id_gm_mater_sz"
                      JOIN "gm_mater" parent ON d."id_gm_mater" = parent."id"
                      JOIN "gm_mater_sz" old
                         ON old."id" = child."id_x_gm_mater_sz"
                      JOIN "gm_kody_kreskowe" m
                         ON (    TRIM (m."numer") = TRIM (old."numer")
                             AND TRIM (parent."symbol") = TRIM (m."symbol"))
                WHERE     TRIM (parent."symbol") NOT IN ('5-9999-999-9999',
                                                         'T-0000-000-0001',
                                                         'T-0000-000-0002',
                                                         'U-0000-000-0002',
                                                         'K-0001-000-0000')
                      AND d."id_gm_obroty" = p_mm_id
                      AND m."kkreskowy" = p_barcode
             GROUP BY m."kkreskowy")
      LOOP
         SELECT COUNT (*)
           INTO v_count1
           FROM loc_out_bufor
          WHERE     OB.ProductBarCode = LOCO_PR_BARCODE
                AND p_mm_id = LOCO_SCHPT_ID;

         IF v_count1 = 0
         THEN
            t_amount := p_count;
         ELSE
            SELECT SUM (loco_pr_amount)
              INTO t_amount
              FROM loc_out_bufor
             WHERE     OB.ProductBarCode = LOCO_PR_BARCODE
                   AND p_mm_id = LOCO_SCHPT_ID;

            t_amount := t_amount + p_count;
         END IF;

         IF (t_amount <= OB.AMOUNT)
         THEN
            v_count := 1;
         ELSE
            v_count := 0;
         END IF;
      END LOOP;

      RETURN v_count;
   END;