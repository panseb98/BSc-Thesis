/* Formatted on 04/06/2020 13:27:54 (QP5 v5.287) */
SELECT parent.pr_Code,
       child.PR_BAR_CODE,
       child.pr_id,
       child.PR_CURRENT_QTY AS KBTRADE,
       TEST_QTY AS KBTRADE_ORG,
       mag.qry2 AS MAG,
       TEST_QTY_MAG AS MAG_ORG,
       MIN_PROD.QTY AS MIN_PROD_QTY,
       PLUS.QTY AS PLUS_PROD_QTY
  FROM (SELECT *
          FROM wb_products
         WHERE     pr_pr_id_parent IS NULL
               AND PR_IS_KB_TRADE = 1
               AND PR_IVE_ID_ORIGINAL = 2849
               AND PR_IS_ACTIVE = 1) parent
       JOIN (SELECT *
               FROM wb_products
              WHERE pr_pr_id_parent IS NOT NULL) child
          ON child.pr_pr_id_parent = parent.pr_id
       LEFT JOIN (  SELECT SUM (LCD_PR_AMOUNT) AS qry2, LCD_PR_BARCODE
                      FROM loc_localization_contents_det
                     WHERE lcd_is_active = 1
                  GROUP BY LCD_PR_BARCODE) mag
          ON mag.LCD_PR_BARCODE = child.PR_BAR_CODE
       LEFT JOIN
       (  SELECT IOD_PR_ID, SUM (IOD_QTY) AS QTY
            FROM ive_inventory_oper_details
           WHERE     IOD_INVO_ID IN
                        (SELECT INVO_ID
                           FROM IVE_INVENTORY_OPERATIONS
                          WHERE     INVO_INVE_ID = 2849
                                AND INVO_IV_CODE_TYPE IN
                                       ('INVO_MM', 'INVO_WZ', 'INVO_R-')
                                AND INVO_DATE =
                                       TO_DATE ('04/06/2020', 'DD/MM/YYYY'))
                 AND IOD_IS_ACTIVE = 1
        GROUP BY IOD_PR_ID) MIN_PROD
          ON MIN_PROD.IOD_PR_ID = child.pr_id
       LEFT JOIN MAG_CMP_QTY_BOH ON child.pr_id = TEST_PR_ID
       LEFT JOIN
       (  SELECT IOD_PR_ID, SUM (IOD_QTY) AS QTY
            FROM ive_inventory_oper_details
           WHERE     IOD_INVO_ID IN
                        (SELECT INVO_ID
                           FROM IVE_INVENTORY_OPERATIONS
                          WHERE     INVO_INVE_ID = 2849
                                AND INVO_IV_CODE_TYPE IN ('INVO_PZ', 'INVO_R+')
                                AND INVO_DATE =
                                       TO_DATE ('04/06/2020', 'DD/MM/YYYY'))
                 AND IOD_IS_ACTIVE = 1
        GROUP BY IOD_PR_ID) PLUS
          ON CHILD.PR_ID = PLUS.IOD_PR_ID;