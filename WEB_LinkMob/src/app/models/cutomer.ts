import { Invoice } from "./invoice";

export interface Customer {
id?: number;
companyName?: string;
address?: string;
state?: string;
country?: string[];
fullAddress?: string[];
subscriptionState: string[2];
invoices: Invoice[];
numberOfInvoices: number;
}