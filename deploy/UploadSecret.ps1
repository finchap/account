$SubscriptionName = "dachapde-internal"
$KeyVaultName = "finchap-dev"
$SecretName = "david-chapdelaineAThotmail-com-50d6c397-77ce-4b56-be51-1780931aeabf"
$Secret = Get-Content ..\src\bank_communication\work\david_chapdelaine@hotmail.com.json -Encoding UTF8 | Out-String | ConvertTo-SecureString -AsPlainText -Force

Try
{
    if((Get-AzureRmContext -ErrorAction Continue).Account -eq $null)
    {
        Login-AzureRmAccount
    }
}
Catch [System.Management.Automation.PSInvalidOperationException]
{
    Login-AzureRmAccount
}

Select-AzureRmSubscription -SubscriptionName $SubscriptionName
Set-AzureKeyVaultSecret -VaultName $KeyVaultName -Name $SecretName -SecretValue $Secret
