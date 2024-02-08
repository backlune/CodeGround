# Structuring tests

AS developers we tend to structure things by code infrastructure instead of funcitonality. Why do we group controllers instead of grouping on a concept like CreditNote and CustomerInvoice?

I think there is a need to those code infrastructure tests (unit tests?) as well but we maybe not only use that?

What if we try to split these concepts?

Is it because when the code base is small it doesn't hurt us so structure by code infrastructure? Why the MVVM folder structure for example. 

## This folder is the basis for trying out a different approach
I will do this around invoice as that is one area that I know well.

The problem I seems to end up in is "What is the higher order concept of two functions?". Say you introduce invoice payments. They might now apply to both Cash and Customer invoice.
How do we figure this out?
Should one thing of it in dependency? 
Payment is more general concept as it can be done from multiple types of invoices but how payments should work might differ depending on if it's a cash or customer invoice.

What do we want?
- Discoverability
- Test case coverage
- Logical where to place new tests


