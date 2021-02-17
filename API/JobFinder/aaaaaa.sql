SELECT parent.pr_Code,
       child.PR_BAR_CODE,
       sum(child.PR_CURRENT_QTY) AS KBTRADE,
       sum(IOD_QTY)
  FROM (SELECT *
          FROM wb_products
         WHERE     pr_pr_id_parent IS NULL
               AND PR_IS_KB_TRADE = 1
               AND PR_IVE_ID_ORIGINAL = 2849
               AND PR_IS_ACTIVE = 1) parent
       JOIN (SELECT *
               FROM wb_products
              WHERE pr_pr_id_parent IS NOT NULL
              AND PR_IVE_ID_ORIGINAL = 2849) child
          ON child.pr_pr_id_parent = parent.pr_id
       join WBV_PROD_FIFO_HISTORY on IOD_PR_ID = child.pr_id
       group by parent.pr_Code,
       child.PR_BAR_CODE;